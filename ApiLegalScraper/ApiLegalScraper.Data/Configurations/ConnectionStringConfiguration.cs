using System;
using System.IO;

namespace LegalScraper.Data.Configurations
{
    public static class ConnectionStringConfiguration
    {
        private static readonly string AppNome = "LegalScraper.Application";
        private static readonly string AppCaminho = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppNome);

        public static string ObterConnectionString()
        {
            EnsureFolderExists(AppCaminho);
            return $"Data Source={AppCaminho}\\legal_scraper_db.sqlite";
        }

        private static string EnsureFolderExists(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath;
        }
    }
}
