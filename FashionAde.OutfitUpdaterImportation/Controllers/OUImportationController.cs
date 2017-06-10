using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using FashionAde.Core.Clothing;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.ThirdParties;
using FashionAde.Core.Trends;
using FashionAde.OutfitUpdaterImportation.Core;
using FashionAde.OutfitUpdaterImportation.Interfaces;
using FileHelpers;
using FileHelpers.RunTime;
using SharpArch.Web.NHibernate;
using SharpArch.Data.NHibernate;
using SharpArch.Core.PersistenceSupport;

namespace FashionAde.OutfitUpdaterImportation.Controllers
{
    /// REVIEW: The class contains no comments.
    public class OUImportationController
    {
        public OUImportationController(IOUImportationClassBuilder ouImportationClassBuilder, IOutfitUpdaterRepository outfitUpdaterRepository, ITrendRepository trendRepository, ISilouhetteRepository silouhetteRepository, IPatternRepository patternRepository, IColorFamilyRepository colorFamilyRepository)
        {
            this.ouImportationClassBuilder = ouImportationClassBuilder;
            this.outfitUpdaterRepository = outfitUpdaterRepository;
            this.trendRepository = trendRepository;
            this.silouhetteRepository = silouhetteRepository;
            this.patternRepository = patternRepository;
            this.colorFamilyRepository = colorFamilyRepository;
        }

        #region Properties
        private List<string> linesWithErrors = new List<string>();
        private List<string> linesOk = new List<string>();
        private HashSet<string> itemIds = new HashSet<string>();
        private string filename;
        private bool haveHeader;
        private string separator;
        private string path = ConfigurationManager.AppSettings["OUImportation_Path"];
        private bool sync;
        private List<OUImportationLine> lines;
        private double memorySafe;
        private int actualLine;
        private int readSet = 1000;
        private Partner partner;
        private IOUImportationClassBuilder ouImportationClassBuilder;
        private List<OutfitUpdater> outfitUpdaters = new List<OutfitUpdater>();

        private List<Trend> trends = new List<Trend>();
        private List<Silouhette> silouhettes = new List<Silouhette>();
        private List<Pattern> patterns = new List<Pattern>();
        private List<ColorFamily> colorFamilies = new List<ColorFamily>();

        private IOutfitUpdaterRepository outfitUpdaterRepository;
        private ITrendRepository trendRepository;
        private ISilouhetteRepository silouhetteRepository;
        private IPatternRepository patternRepository;
        private IColorFamilyRepository colorFamilyRepository;

        private IList<ColorFamilyKeywordsByPartner> colorFamilyKeywords;
        private IList<PatternKeywordsByPartner> patternKeywords;
        private IList<SilouhetteKeywordsByPartner> silouhetteKeywords;

        private List<string> wordsForDiscard = new List<string>();

        public string Process
        {
            get
            {
                if(sync)
                    return "Sync";
                return "Async";
            }
        }

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        public bool HaveHeader
        {
            get { return haveHeader; }
            set { haveHeader = value; }
        }

        public string Separator
        {
            get { return separator; }
            set { separator = value; }
        }

        public bool Sync
        {
            get { return sync; }
            set { sync = value; }
        }

        public double MemorySafe
        {
            get { return memorySafe; }
            set { memorySafe = value / 100; }
        }

        public int TotalErrors
        {
            get { return linesWithErrors.Count; }
        }

        public Partner Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        #endregion

        // REVIEW: Why do we need these limits????? Memory should be cleared when there is too much use.
        // REVIEW: We should process the file in a tandem of records. There should be no need to limit memory.
        // REVIEW: The amount of records to be processed on the batch should be configurable, we could use much
        // REVIEW: records on a good server.
        #region Performance Counters
        // Page Level declaration
        protected PerformanceCounter cpuCounter;
        protected PerformanceCounter ramCounter;
        protected ulong ramTotal = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1024 / 1024;

        private void InitializePerformanceCounters()
        {
            cpuCounter = new PerformanceCounter();
            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

        }

        // Call this method every time you need to know the current cpu usage. 
        private string getCurrentCpuUsage()
        {
            return cpuCounter.NextValue().ToString("0.000") + "%";
        }

        // Call this method every time you need to get the amount of the available RAM in Mb 
        private string getAvailableRAM()
        {
            return ramCounter.NextValue() + "MB";
        }
        #endregion

        public void Create()
        {
            InitializePerformanceCounters();
            if(ramCounter.NextValue() < (ramTotal * MemorySafe))
            {
                WriteInfo(string.Format("Safe Memory Limit"), string.Format("Available = {0} < Safe Limit = {1}MB", getAvailableRAM(), ramTotal * MemorySafe));
                return;
            }

            DateTime startTime = DateTime.Now;
            int totalRecords = 0;
            lines = new List<OUImportationLine>();

            // TODO: Fetch keywords to make it faster on the loops.
            trends = new List<Trend>(trendRepository.GetAll());
            silouhettes = new List<Silouhette>(silouhetteRepository.GetAll());
            patterns = new List<Pattern>(patternRepository.GetAll());
            colorFamilies = new List<ColorFamily>(colorFamilyRepository.GetAll());
            outfitUpdaters = new List<OutfitUpdater>(outfitUpdaterRepository.GetFor(Partner));
            wordsForDiscard.Add(" men");
            wordsForDiscard.Add(" mens");
            wordsForDiscard.Add(" men's");
            wordsForDiscard.Add(" men´s");
            wordsForDiscard.Add("kid");
            wordsForDiscard.Add("kids");
            wordsForDiscard.Add("kid's");
            wordsForDiscard.Add("kid´s");

            // Retrieve Keywords
            // TODO: Limit by partner
            colorFamilyKeywords = new Repository<ColorFamilyKeywordsByPartner>().GetAll();
            patternKeywords = new Repository<PatternKeywordsByPartner>().GetAll();
            silouhetteKeywords = new Repository<SilouhetteKeywordsByPartner>().GetAll();

            Write("--------------------------------------------------------------------------------");
            Write(string.Format("{0} - {1}\t\t{2}\t\t{3}\t{4}\t{5}", "Time", "Process Type", "Action", "CPU Usage", "RAM Available(MB)", "Records OK"));
            WriteFullInfo("Start");

            //REVIEW: The ZapposClassBuilder should read the file and create an event for the loop, returning an
            //REVIEW: understandable object that we can interchange when we have another builder. This way we can not 
            //REVIEW: change the builder in a truly manner. Right now, is kind of complex and not clear how to change it.
            //REVIEW: That way is simple to change from reading from a file or other providers.
            //REVIEW: This class should worry about checking if the line is valid and import it in our system.
            DelimitedClassBuilder cb = ouImportationClassBuilder.CreateClassBuilder(Separator, HaveHeader);

            //REVIEW: Why is these here? We always read line at line. 
            if (Sync)
            {
                FileHelperEngine engine = new FileHelperEngine(cb.CreateRecordClass());
                object[] items = engine.ReadFile(path + Filename);
                totalRecords = items.Length;
                WriteFullInfo("Ready");

                for (int i = 0; i < items.Length; i++)
                {
                    actualLine = i;
                    if (HaveHeader)
                        actualLine++;

                    ProcessOUImportationLine(items[i]);
                    
                    CheckLimits();
                }
            }
            else
            {
                FileHelperAsyncEngine engine = new FileHelperAsyncEngine(cb.CreateRecordClass());
                engine.BeginReadFile(path + Filename);
                WriteFullInfo("Ready");

                while (engine.ReadNext() != null)
                {
                    actualLine = totalRecords + 1;
                    if (HaveHeader)
                        actualLine++;

                    ProcessOUImportationLine(engine.LastRecord);
                    
                    CheckLimits();
                    totalRecords++;
                }

                ProcessList();
            }

            DateTime endTime = DateTime.Now;
            TimeSpan span = endTime - startTime;
            WriteFullInfo("Finish");
            Write("--------------------------------------------------------------------------------");
            Write(string.Format("{0} - {1} {2} {3} records in {4} seconds", endTime.ToLongTimeString(), Process, "Finish", totalRecords, span.TotalSeconds.ToString("0")));
            WriteInfo("Total lines added/modified", linesOk.Count.ToString());
            WriteInfo("Total errors", TotalErrors.ToString());
            //WriteInfo("Lines with errors", string.Join(",", linesWithErrors.ToArray()));
            Write("--------------------------------------------------------------------------------");
        }

        private void CheckLimits()
        {
            if (actualLine % readSet == 0)
            {
                WriteFullInfo(actualLine.ToString("#,##0"));
                ProcessList();
            }

            if(!sync && ramCounter.NextValue() < (ramTotal * MemorySafe))
            {
                WriteInfo(string.Format("Safe Memory Limit Reached ({0}MB)", ramTotal * MemorySafe), string.Format("Processing {0} records", lines.Count));
                ProcessList();
                WriteInfo(string.Format("Safe Memory Limit Reached ({0}MB)", ramTotal * MemorySafe), string.Format("Cleaning Memory"));
            }
        }

        //REVIEW: Avoid these methods, use log4net directly everywhere.
        private static void Write(string text)
        {
            Console.WriteLine(text);
        }

        private static void WriteInfo(string title, string detail)
        {
            Write(string.Format("{0} - {1}: {2}", DateTime.Now.ToLongTimeString(), title, detail));
        }

        private void WriteFullInfo(string action)
        {
            Write(string.Format("{0} - {1}\t{2}\t\t{3}\t\t{4}\t\t\t\t{5}", DateTime.Now.ToLongTimeString(), Process, action, getCurrentCpuUsage(), getAvailableRAM(), linesOk.Count));
        }

        //REVIEW: Why are we using a generic object?????
        private void ProcessOUImportationLine(object data)
        {
            try
            {
                OUImportationLine line = GetOUImportationLine(data);
                if (line != null)
                    lines.Add(line);
                else
                    linesWithErrors.Add(actualLine.ToString());
            }
            catch (Exception)
            {
                linesWithErrors.Add(actualLine.ToString());
            }
        }

        // REVIEW: This makes no sense. We should always receive a real object from the Builder.
        private OUImportationLine GetOUImportationLine(object data)
        {
            IFormatProvider formatProvider = new CultureInfo("en-US");

            OUImportationLine line = new OUImportationLine();
            line.ProgramName = GetValue("ProgramName", data).ToString();
            line.Name = GetValue("Name", data).ToString();
            line.Description = GetValue("Description", data).ToString();
            line.Keywords= GetValue("Keywords", data).ToString();
            line.Sku = GetValue("Sku", data).ToString();
            if (!string.IsNullOrEmpty(GetValue("Price", data).ToString()))
                line.Price = Convert.ToDecimal(GetValue("Price", data).ToString(), formatProvider);
            line.BuyUrl = GetValue("BuyUrl", data).ToString();
            line.ImageUrl = GetValue("ImageUrl", data).ToString();
            return CheckLine(line);
        }

        private object GetValue(string property, object o)
        {
            return o.GetType().GetField(property).GetValue(o);
        }

        private OUImportationLine CheckLine(OUImportationLine line)
        {
            //Chekeo de datos obligatorios
            if (string.IsNullOrEmpty(line.Sku) || string.IsNullOrEmpty(line.ProgramName)
                || string.IsNullOrEmpty(line.Description) || string.IsNullOrEmpty(line.Keywords) 
                || line.Price == null || line.Price <= 0
                || string.IsNullOrEmpty(line.BuyUrl) || string.IsNullOrEmpty(line.ImageUrl))
            {
                return null;
            }

            string lineForKeywords = string.Format("{0} {1} {2}", line.Name.ToLower(), line.Keywords.ToLower(), line.Description.ToLower());

            foreach (string s in wordsForDiscard)
            {
                if (lineForKeywords.Contains(s.ToLower()))
                    return null;
            }
            
            // REVIEW: LINQ queries are more efficient.
            foreach (Trend trend in trends)
                foreach (string keyword in trend.Keywords)
                {
                    if (lineForKeywords.Contains(keyword.ToLower()))
                    {
                        line.Trends.Add(trend);
                        break;
                    }
                }

            if (line.Trends.Count == 0)
                return null;

            // REVIEW: LINQ queries are more efficient.
            foreach (Silouhette silouhette in silouhettes)
                if (lineForKeywords.Contains(silouhette.Description.ToLower()))
                {
                    line.Silouhette = silouhette;
                    break;
                }
                else
                {
                    foreach (SilouhetteKeywordsByPartner keyword in silouhetteKeywords)
                    {
                        if (keyword.Partner == partner && keyword.Silouhette == silouhette)
                        {
                            foreach (string key in keyword.Keywords.Split(','))
                                if (lineForKeywords.Contains(key.Trim().ToLower()))
                                {
                                    line.Silouhette = silouhette;
                                    // REVIEW: This will lead to continue searching other keywords
                                    // REVIEW: We are only leaving the inner for.
                                    break;
                                }    
                        }
                    }
                }

            // REVIEW: LINQ queries are more efficient.
            foreach (Pattern pattern in patterns)
                if (lineForKeywords.Contains(pattern.Description.ToLower()))
                {
                    line.Pattern = pattern;
                    break;
                }
                else
                {
                    foreach (PatternKeywordsByPartner keyword in patternKeywords)
                        if (keyword.Partner == partner && keyword.Pattern == pattern)
                            foreach (string key in keyword.Keywords.Split(','))
                                if (lineForKeywords.Contains(key.Trim().ToLower()))
                                {
                                    line.Pattern = pattern;
                                    // REVIEW: This will lead to continue searching other keywords
                                    // REVIEW: We are only leaving the inner for.
                                    break;
                                }
                }

            // REVIEW: LINQ queries are more efficient.
            foreach (ColorFamily colorFamily in colorFamilies)
                if (lineForKeywords.Contains(colorFamily.Description.ToLower()))
                {
                    line.ColorFamily = colorFamily;
                    break;
                }
                else
                {
                    foreach (ColorFamilyKeywordsByPartner keyword in colorFamilyKeywords)
                        if (keyword.Partner == partner && keyword.ColorFamily == colorFamily)
                            foreach (string key in keyword.Keywords.Split(','))
                                if (lineForKeywords.Contains(key.Trim().ToLower()))
                                {
                                    line.ColorFamily = colorFamily;
                                    // REVIEW: This will lead to continue searching other keywords
                                    // REVIEW: We are only leaving the inner for.
                                    break;
                                }
                }

            if (line.Silouhette != null && line.Pattern != null && line.ColorFamily != null)
                line.Status = OutfitUpdaterStatus.Valid;

            int tmpCount = itemIds.Count;
            itemIds.Add(line.Sku);
            if (itemIds.Count == tmpCount)
                return null;
            
            return line;
        }

        private void ProcessList()
        {
            outfitUpdaterRepository.DbContext.BeginTransaction();
            foreach (OUImportationLine importationLine in lines)
            {
                OutfitUpdater outfitUpdater = outfitUpdaters.Find(e => e.ExternalId.Equals(importationLine.Sku));
                if(outfitUpdater == null)
                    outfitUpdater = new OutfitUpdater();
                if ((outfitUpdater.BuyUrl == importationLine.BuyUrl) && (outfitUpdater.Description == importationLine.Description)
                    && (outfitUpdater.ExternalId == importationLine.Sku) && (outfitUpdater.ImageUrl == importationLine.ImageUrl)
                    && (outfitUpdater.Keywords == importationLine.Keywords) && (outfitUpdater.Name == importationLine.Name)
                    && (outfitUpdater.Price == importationLine.Price) && (outfitUpdater.Trends == importationLine.Trends)
                    && (outfitUpdater.Partner == Partner) && (outfitUpdater.Silouhette == importationLine.Silouhette)
                    && (outfitUpdater.Pattern == importationLine.Pattern) && (outfitUpdater.ColorFamily == importationLine.ColorFamily))
                    continue;
                outfitUpdater.BuyUrl = importationLine.BuyUrl;
                outfitUpdater.Description = importationLine.Description;
                outfitUpdater.ExternalId = importationLine.Sku;
                outfitUpdater.ImageUrl = importationLine.ImageUrl;
                outfitUpdater.Keywords = importationLine.Keywords;
                outfitUpdater.Name = importationLine.Name;
                outfitUpdater.Partner = Partner;
                outfitUpdater.Price = importationLine.Price;
                outfitUpdater.Trends = importationLine.Trends;

                if (outfitUpdater.Status == OutfitUpdaterStatus.Incomplete)
                {
                    if (outfitUpdater.Silouhette == null)
                        outfitUpdater.Silouhette = importationLine.Silouhette;
                    if (outfitUpdater.Pattern == null)
                        outfitUpdater.Pattern = importationLine.Pattern;
                    if (outfitUpdater.ColorFamily == null)
                        outfitUpdater.ColorFamily = importationLine.ColorFamily;
                    outfitUpdater.Status = importationLine.Status;
                }
                
                outfitUpdaterRepository.SaveOrUpdate(outfitUpdater);
                linesOk.Add(actualLine.ToString());
            }
            outfitUpdaterRepository.DbContext.CommitTransaction();
            lines = new List<OUImportationLine>();
            GC.Collect();
        }
    }
}
