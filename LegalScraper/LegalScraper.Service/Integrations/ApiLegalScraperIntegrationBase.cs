using LegalScraper.Domain.Configurations;
using LegalScraper.Domain.Models;
using LegalScraper.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LegalScraper.Service.Integrations
{
    public abstract class ApiLegalScraperIntegrationBase
    {

        private HttpClient _httpClient = new HttpClient();
        public ApiLegalScraperIntegrationBase() 
        {
            _httpClient.BaseAddress = new Uri(Configuration.UrlApiBase);
        }

        private StringContent GetPayload(object payload)
        {
            return new StringContent(
                JsonConvert.SerializeObject(payload, new JsonSerializerSettings()),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> Get<T>(string urlPath, int id)
        {
            var response = _httpClient.GetAsync($"{urlPath}/{id}");
            response.Wait();

            if (response.IsCompleted)
            {
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var menssage = result.Content.ReadAsStringAsync();
                    menssage.Wait();
                    var obj = JsonConvert.DeserializeObject<T>(menssage.Result);

                    return obj;
                }
                else
                {
                    string conteudo = await result.Content.ReadAsStringAsync();
                    Log.EscreverLog($"Erro Retorno Api StatusCode: {result.StatusCode} - Content: {conteudo}", Log.TipoLog.Error);
                }
            }

            return default(T);
        }

        protected async Task<List<T>> Get<T>(string urlPath)
        {
            var response = _httpClient.GetAsync(urlPath);
            response.Wait();

            if (response.IsCompleted)
            {
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var menssage = result.Content.ReadAsStringAsync();
                    menssage.Wait();
                    List<T> list = JsonConvert.DeserializeObject<List<T>>(menssage.Result);
                    
                    return list;   
                }
                else
                {
                    string conteudo = await result.Content.ReadAsStringAsync();
                    Log.EscreverLog($"Erro Retorno Api StatusCode: {result.StatusCode} - Content: {conteudo}", Log.TipoLog.Error);
                }
            }
            
            return default(List<T>);
        }
        

        protected async Task<T> Post<T>(string urlPath, object payload)
        {
            var response = _httpClient.PostAsync(urlPath, GetPayload(payload));
            response.Wait();

            if (response.IsCompleted)
            {
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    string message = await result.Content.ReadAsStringAsync();
                    var body = JsonConvert.DeserializeObject<T>(message);
                    return body;
                }
                else
                {
                    string conteudo = await result.Content.ReadAsStringAsync();
                    Log.EscreverLog($"Erro Retorno Api StatusCode: {result.StatusCode} - Content: {conteudo}", Log.TipoLog.Error);
                }
            }
            return default(T);
        }

        public async Task<bool> Put<T>(string urlPath, object payload)
        {
            var response =  _httpClient.PutAsync(urlPath,GetPayload(payload));
            response.Wait();

            if (response.IsCompleted)
            {
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    string conteudo = await result.Content.ReadAsStringAsync();
                    return true;
                }
                else
                {
                    string conteudo = await result.Content.ReadAsStringAsync();
                    Log.EscreverLog($"Erro Retorno Api StatusCode: {result.StatusCode} - Content: {conteudo}", Log.TipoLog.Error);
                }
            }
            return false;
        }

        public async Task<bool> Delete<T>(string urlPath, int id)
        {
            try
            {
                var response = _httpClient.DeleteAsync($"{urlPath}/{id}");
                response.Wait();

                if (response.IsCompleted)
                {
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string conteudo = await result.Content.ReadAsStringAsync();
                        return true;
                    }
                    else
                    {
                        string conteudo = await result.Content.ReadAsStringAsync();
                        Log.EscreverLog($"Erro Retorno Api StatusCode: {result.StatusCode} - Content: {conteudo}", Log.TipoLog.Error);
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
            
        }


        public abstract Task<T> GetEntity<T>(string urlPath, int id);
        public abstract Task<List<T>> GetEntity<T>(string urlPath);
        public abstract Task<T> CreateEntity<T>(string urlPath, object processo);
        public abstract Task<bool> UpdateEntity<T>(string urlPath, object processo);
        public abstract Task<bool> DeleteEntity<T>(string urlPath, int id);

    }
}

