using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTPConnector;
using System.IO;
using System.IO.Compression;
using System.Configuration;
using log4net;

namespace FashionAde.OutfitUpdaterExecutor
{
    class Program
    {
        /// <summary>
        /// Updates all feeds.
        /// Normally executed on weekends.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            try {

                 /*           
                Server: aftp.linksynergy.com
                Username: jfennell2753
                Password: KftEapMS
                */

                // Download all needed files
                string host = "aftp.linksynergy.com";
                string userName = "jfennell2753";
                string password = "KftEapMS";
                string sourceFile = "13816_2389515_mp.xml.gz";
//                string sourceFile = "24895_2389515_mp.xml.gz";

                //string host = "ftp.nybblegroup.com";
                //string userName = "ftpac";
                //string password = "jody23a";
                //string sourceFile = "Zappos_com-Product_Catalog_1.txt.gz";

                string destFile = Path.Combine(ConfigurationManager.AppSettings["OUImportation_Path"], sourceFile);

                GetLogger().Debug("Backup file...");
                // Backup file if exists
                if (File.Exists(destFile))
                    File.Move(destFile, string.Format(destFile + ".{0}.bak", DateTime.Now.ToString("yyyyMMdd_hhmmss")));

                GetLogger().Debug("Download File...");

                FTPClient.FTPConnection conn = new FTPClient.FTPConnection();
                conn.Open(host, userName, password, FTPClient.FTPMode.Active);
                conn.GetFile(sourceFile, destFile, FTPClient.FTPFileTransferType.Binary);

                // Download file from FTP
                //FTPclient client = new FTPclient();
                //client.Hostname = host;
                //client.Username = userName;
                //client.Password = password;
                //client.Download(sourceFile, destFile, true);

                GetLogger().Debug("Decompress File...");
                // Is compressed?
                if (destFile.Contains(".gz"))
                {
                    // Backup file if exists
                    if (File.Exists(destFile.Replace(".gz", "")))
                        File.Move(destFile.Replace(".gz", ""), string.Format(destFile.Replace(".gz", "") + ".{0}.bak", DateTime.Now.ToString("yyyyMMdd_hhmm")));

                    // Decompress file
                    DecompressFile(destFile);
                }

                // Start executing the updater
                //new OutfitUpdaterReference.OutfitUpdaterServiceClient().UpdateFeeds();
            }
            catch (Exception ex) 
            {
                GetLogger().Error(ex);
            }
        }

        private static ILog GetLogger()
        {
            return LogManager.GetLogger("FashionAde.OutfitUpdaterExecutor");
        }


        private static void DecompressFile(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            FileStream fsOutput = new FileStream(file.Replace(".gz",""),
                                                 FileMode.Create,
                                                 FileAccess.Write);
            GZipStream zip = new GZipStream(fs, CompressionMode.Decompress, true);

            byte[] buffer = new byte[4096];
            int bytesRead;
            bool continueLoop = true;
            while (continueLoop)
            {
                bytesRead = zip.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                    break;
                fsOutput.Write(buffer, 0, bytesRead);
            }
            zip.Close();
            fsOutput.Close();
            fs.Close();
        }
    }

}
