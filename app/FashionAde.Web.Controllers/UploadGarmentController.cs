using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using System.Collections;
using FashionAde.Core.Accounts;
using FashionAde.Core.Clothing;
using System.Web;
using SharpArch.Web.NHibernate;
using System.IO;
using FashionAde.Core;
using FashionAde.Web.Controllers.MVCInteraction;
using FashionAde.Core.MVCInteraction;
using FashionAde.Core.DataInterfaces;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Specialized;
using FashionAde.Web.Common;
using FashionAde.Utils;
using System.Configuration;

namespace FashionAde.Web.Controllers
{
    [HandleError]
    [Authorize]
    public class UploadGarmentController : BaseController
    {
        private IClosetRepository closetRepository;
        private IGarmentRepository garmentRepository;
        private IUserGarmentRepository userGarmentRepository;
        private IColorRepository colorRepository;
        private IPatternRepository patternRepository;
        private ISilouhetteRepository silouhetteRepository;
        private IFabricRepository fabricRepository;
        private IEventTypeRepository eventTypeRepository;
        private IPreGarmentRepository pregarmentRepository;

        public UploadGarmentController(IClosetRepository closetRepository, IGarmentRepository garmentRepository, IUserGarmentRepository userGarmentRepository, IColorRepository colorRepository, IPatternRepository patternRepository, ISilouhetteRepository silouhetteRepository, IFabricRepository fabricRepository, IEventTypeRepository eventTypeRepository, IPreGarmentRepository pregarmentRepository)
        {
            this.closetRepository = closetRepository;
            this.garmentRepository = garmentRepository;
            this.userGarmentRepository = userGarmentRepository;
            this.colorRepository = colorRepository;
            this.patternRepository = patternRepository;
            this.silouhetteRepository = silouhetteRepository;
            this.fabricRepository = fabricRepository;
            this.eventTypeRepository = eventTypeRepository;
            this.pregarmentRepository = pregarmentRepository;
        }

        [ObjectFilter(Param = "selection", RootType = typeof(jsonLayer))]
        public ActionResult GetLayers(jsonLayer selection)
        {
            IList<LayerResponse> lst = garmentRepository.GetLayers(selection.GarmentIds);
            return Json(new { Success = true, Layers = lst });
        }

        public ActionResult UploadFile(FormCollection values)
        {
            ArrayList lst = GetFormatedValues(values);
            RegisteredUser user = this.ProxyLoggedUser;
            IList<UserGarment> lstFiles = new List<UserGarment>();

            List<int> garmentsIds = new List<int>();
            userGarmentRepository.DbContext.BeginTransaction();

            for (int i = 0; i < Request.Files.Count - 1; i++)
            {
                HttpPostedFileBase uploadedFile = Request.Files[i];
                if (uploadedFile.ContentLength != 0)
                {
                    UserGarment ug = (UserGarment)lst[i];
                    ug.User = user;
                    ug.ImageUri = "";
                    ug.LinkUri = "";

                    // Find pregarment
                    IDictionary<string, object> propertyValues = new Dictionary<string, object>();
                    propertyValues.Add("Silouhette", ug.Tags.Silouhette);
                    propertyValues.Add("PatternType", ug.Tags.Pattern.Type);
                    propertyValues.Add("ColorFamily", ug.Tags.DefaultColor.Family);
                    ug.PreGarment = pregarmentRepository.FindOne(propertyValues);
                    ug.UpdateSeasonCode();
                    ug.UpdateEventTypeCode();
                    userGarmentRepository.SaveOrUpdate(ug);

                    FileInfo fi = new FileInfo(uploadedFile.FileName);
                    string fileName = "user_" + ug.Id.ToString() + fi.Extension;

                    string path = ConfigurationManager.AppSettings["Resources_Path"];
                    string filePath = Path.Combine(Path.Combine(path, @"Garments\UploadedImages\"), fileName);
                    string smallImgPath = Path.Combine(Path.Combine(path, @"Garments\65\"), fileName);
                    string largelImgPath = Path.Combine(Path.Combine(path, @"Garments\95\"), fileName);

                    uploadedFile.SaveAs(filePath);

                    // TODO: Improve borders.
                    ImageHelper.MakeTransparent(filePath);
                    
                    ResizeImage(filePath, largelImgPath, 135, 95, true);  //Imagenes Grandes
                    ResizeImage(filePath, smallImgPath, 65, 65, true);  //Imagenes Pequeñas

                    ug.ImageUri = fileName;
                    userGarmentRepository.SaveOrUpdate(ug);
                    lstFiles.Add(ug);

                    Closet closet = closetRepository.Get(this.ClosetId);
                    closet.AddGarment(ug);
                    closetRepository.SaveOrUpdate(closet);

                    garmentsIds.Add(ug.Id);
                }
            }

            userGarmentRepository.DbContext.CommitTransaction();

            new FashionAde.Utils.OutfitEngineService.OutfitEngineServiceClient().AddOutfits(user.Closet.Id, garmentsIds);

            ViewData["uploadedFiles"] = lstFiles;
            return View();
        }

        public ActionResult Index()
        {
            RegisteredUser user = this.ProxyLoggedUser;

            IList<FashionAde.Core.Clothing.Color> colors = colorRepository.GetAll();
            List<SelectListItem> lstcolors = new List<SelectListItem>();

            foreach (FashionAde.Core.Clothing.Color color in colors)
            {
                SelectListItem sl = new SelectListItem();
                sl.Text = color.Description;
                sl.Value = color.Id.ToString();
                lstcolors.Add(sl);
            }
            ViewData["Colors"] = lstcolors;

            IList<Pattern> patterns = patternRepository.GetAll();
            List<SelectListItem> lstPatterns = new List<SelectListItem>();

            foreach (Pattern pattern in patterns)
            {
                SelectListItem sl = new SelectListItem();
                sl.Text = pattern.Description;
                sl.Value = pattern.Id.ToString();
                lstPatterns.Add(sl);
            }
            ViewData["Patterns"] = lstPatterns;

            IList<Fabric> fabrics = fabricRepository.GetAll();
            List<SelectListItem> lstFabrics = new List<SelectListItem>();

            foreach (Fabric fabric in fabrics)
            {
                SelectListItem sl = new SelectListItem();
                sl.Text = fabric.Description;
                sl.Value = fabric.Id.ToString();
                lstFabrics.Add(sl);
            }
            ViewData["Fabrics"] = lstFabrics;

            IList<Silouhette> silouhettes = silouhetteRepository.GetAll();
            List<SelectListItem> lstsilouhettes = new List<SelectListItem>();

            foreach (Silouhette silouhette in silouhettes)
            {
                SelectListItem sl = new SelectListItem();
                
                sl.Text = silouhette.Category.Description + " " + silouhette.Description;
                sl.Value = silouhette.Id.ToString();
                lstsilouhettes.Add(sl);
            }
            ViewData["Titles"] = lstsilouhettes;

            IList<EventType> eventTypes = eventTypeRepository.GetAll();
            List<SelectListItem> lsteventTypes = new List<SelectListItem>();

            foreach (EventType type in eventTypes)
            {
                SelectListItem sl = new SelectListItem();
                sl.Text = type.Description;
                sl.Value = type.Id.ToString();
                lsteventTypes.Add(sl);
            }
            ViewData["EventTypes"] = lsteventTypes;

            LoadSeasons();

            return View();
        }

        /// <summary>
        /// Carga todas las temporadas en el ViewData Seasons.
        /// </summary>
        private void LoadSeasons()
        {
            List<SelectListItem> lstSeasons = new List<SelectListItem>();
            
            SelectListItem season = new SelectListItem();            
            season.Text = Season.Fall.ToString();
            season.Value = ((int)Season.Fall).ToString();
            lstSeasons.Add(season);
            
            season = new SelectListItem();
            season.Text = Season.Spring.ToString();
            season.Value = ((int)Season.Spring).ToString();
            lstSeasons.Add(season);
            
            season = new SelectListItem();
            season.Text = Season.Summer.ToString();
            season.Value = ((int)Season.Summer).ToString();
            lstSeasons.Add(season);
            
            season = new SelectListItem();
            season.Text = Season.Winter.ToString();
            season.Value = ((int)Season.Winter).ToString();            
            lstSeasons.Add(season);

            season = new SelectListItem();
            season.Text = Season.All.ToString();
            season.Value = ((int)Season.All).ToString();
            lstSeasons.Add(season);            
            
            ViewData["Seasons"] = lstSeasons;
        }

        /// <summary>
        /// Guarda la imagen "recortada" por el usuario como una nueva imagen.        
        /// </summary>
        /// <param name="coords">Objeto armado del lado del cliente que contiene las coordenas seleccionadas
        /// por el usuario y el nombre del archivo.</param>
        /// <returns></returns>
        [ObjectFilter(Param = "coords", RootType = typeof(Coords))]
        public ActionResult CropImage(Coords coords)
        {
            var X = coords.X;
            var Y = coords.Y;
            var Width = coords.Width;
            var Height = coords.Height;

            try
            {
                UriBuilder ub = new UriBuilder(coords.ImageUri);

                string path = ConfigurationManager.AppSettings["Resources_Path"];
                string filePath = Path.Combine(Path.Combine(path, @"Garments\UploadedImages\"), Path.GetFileName(ub.Path.Substring(ub.Path.LastIndexOf('/') + 1)));

                using (Image OriginalImage = Image.FromFile(filePath))
                {
                    using (Bitmap bmp = new Bitmap(Width, Height))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                        using (Graphics Graphic = Graphics.FromImage(bmp))
                        {
                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, Width, Height), X, Y, Width, Height, GraphicsUnit.Pixel);
                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, OriginalImage.RawFormat);
                            byte[] CropImage = ms.GetBuffer();

                            using (MemoryStream mstream = new MemoryStream(CropImage, 0, CropImage.Length))
                            {
                                mstream.Write(CropImage, 0, CropImage.Length);
                                using (Image CroppedImage = Image.FromStream(mstream, true))
                                {
                                    string smallImgPath = Path.Combine(Path.Combine(path, @"Garments\65\"), Path.GetFileName(coords.ImageUri));
                                    string largelImgPath = Path.Combine(Path.Combine(path, @"Garments\95\"), Path.GetFileName(coords.ImageUri));

                                    //TODO: Change image name like user_1231321_1.jpg where 1 is the version to disable caching.
                                    //TODO: Update image name on garment data.

                                    OriginalImage.Dispose();
                                    CroppedImage.Save(filePath, CroppedImage.RawFormat);
                                    ResizeImage(filePath, smallImgPath, 135, 100, true);  //Imagenes Grandes
                                    ResizeImage(filePath, largelImgPath, 65, 65, true);  //Imagenes Pequeñas
                                }
                            }
                        }
                    }
                }
                return Json(new { Success = true });
            }
            catch
            {
                return Json(new { Success = false });
            }
        }

        public void ResizeImage(string OriginalFile, string NewFile, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
        {
            System.Drawing.Image FullsizeImage = System.Drawing.Image.FromFile(OriginalFile);

            // Prevent using images internal thumbnail
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (OnlyResizeIfWider)
            {
                if (FullsizeImage.Width <= NewWidth)
                {
                    NewWidth = FullsizeImage.Width;
                }
            }

            int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
            if (NewHeight > MaxHeight)
            {
                // Resize with height instead
                NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
                NewHeight = MaxHeight;
            }

            System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            FullsizeImage.Dispose();

            // Save resized picture
            NewImage.Save(NewFile);
        }

        private ArrayList GetFormatedValues(FormCollection values)
        {
            ArrayList lst = new ArrayList();
            UserGarment ug = new UserGarment();

            if (values.Count == 0)
                return lst;

            int controlId = int.Parse(((NameValueCollection)(values)).AllKeys[0].Substring(
                                        ((NameValueCollection)(values)).AllKeys[0].Length - 1, 1));

            string titleSilouhette = string.Empty;
            string titleCategory = string.Empty;
            string titleColor = string.Empty;
            string titlePattern = string.Empty;
            string titleFabric = string.Empty;

            foreach (string tag in values)
            {
                if (tag == "x" || tag == "y")
                    continue;

                if (int.Parse(tag.Substring(tag.Length - 1)) != controlId)
                {
                    ug.Title = string.Format("{0} {1} {2} {3} {4}", titleSilouhette, titleColor, titlePattern, titleCategory, titleFabric);
                    lst.Add(ug);
                    ug = new UserGarment();
                    titleSilouhette = string.Empty;
                    titleCategory = string.Empty;
                    titleColor = string.Empty;
                    titlePattern = string.Empty;
                    titleFabric = string.Empty;
                    controlId++;
                }

                string propName = tag.Substring(0, tag.Length - 1);
                string propValue = values[tag.ToString()];

                if (propValue == "")
                    continue;

                switch (propName)
                {
                    case "Title":
                        Silouhette s = silouhetteRepository.Get(int.Parse(propValue));
                        titleSilouhette = s.Description;
                        titleCategory = s.Category.Description;
                        ug.Tags.Silouhette = s;
                        break;

                    case "Fabric":
                        Fabric f = fabricRepository.Get(int.Parse(propValue));
                        ug.Tags.Fabric = f;
                        titleFabric = f.Description;
                        break;

                    case "PrimaryColor":
                        FashionAde.Core.Clothing.Color col = colorRepository.Get(int.Parse(propValue));
                        ug.Tags.Colors.Add(col);
                        ug.Tags.DefaultColor = col;
                        titleColor = col.Description;
                        break;

                    case "Pattern":
                        Pattern p = patternRepository.Get(int.Parse(propValue));
                        ug.Tags.Pattern = p;
                        titlePattern = p.Description;
                        break;

                    case "Season":
                        ug.Tags.Seasons.Add((Season)int.Parse(propValue));
                        break;

                    case "EventType":
                        ug.Tags.EventTypes.Add(eventTypeRepository.Get(int.Parse(propValue)));
                        break;

                    case "PrivacyStatus":
                        ug.SetVisibility((Convert.ToBoolean(propValue)) ? GarmentVisibility.Private : GarmentVisibility.Public);
                        break;
                }
            }
            ug.Title = string.Format("{0} {1} {2} {3} {4}", titleSilouhette, titleColor, titlePattern, titleCategory, titleFabric);

            if(ug.Title != "    ")
                lst.Add(ug);
            return lst;
        }

    }
}
