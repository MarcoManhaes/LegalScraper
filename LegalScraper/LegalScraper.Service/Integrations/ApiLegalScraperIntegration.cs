using LegalScraper.Domain.Configurations;
using LegalScraper.Domain.Interfaces;
using LegalScraper.Domain.Models;
using LegalScraper.Service.Services;
using LegalScraper.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace LegalScraper.Service.Integrations
{
    public class ApiLegalScraperIntegration : ApiLegalScraperIntegrationBase
    {
        //private readonly ProcessoIntegration _processoIntegration;

        public ApiLegalScraperIntegration()
        {
           // _processoIntegration = new ProcessoIntegration();
        }

        public override async Task<T> GetEntity<T>(string urlPath, int id)
        {
            return await Get<T>(urlPath, id);
        }

        public override async Task<List<T>> GetEntity<T>(string urlPath)
        {
            return await Get<T>(urlPath);
        }

        public override async Task<T> CreateEntity<T>(string urlPath, object processo)
        {
            return await Post<T>(urlPath, processo);
        }

        public override async Task<bool> UpdateEntity<T>(string urlPath, object processo)
        {
            return await Put<bool>(urlPath, processo);
        }

        public override async Task<bool> DeleteEntity<T>(string urlPath, int id)
        {
            return await Delete<bool>(urlPath, id);
        }
    }
}
