using LegalScraper.Utility;
using LegalScraper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LegalScraper.Service.Integrations;
using LegalScraper.Domain.Interfaces;
using System.Reflection;

namespace LegalScraper.Service.Services
{
    public class ProcessoIntegration : IProcessoIntegration
    {
        private readonly ApiLegalScraperIntegrationBase _apiLegalScraperIntegration;

        public ProcessoIntegration(ApiLegalScraperIntegrationBase apiLegalScraperIntegration)
        {
            _apiLegalScraperIntegration = apiLegalScraperIntegration;
        }

        public async Task<Processo> GetProcesso(int id)
        {
            Log.EscreverLog($"Chamou o método - Task<Processo> GetProcesso(int id)", Log.TipoLog.Trace);
            Log.EscreverLog("Consumindo ApiLegalScraper - Endpoint Get (/api/Processo/{id})", Log.TipoLog.Trace);

            return await _apiLegalScraperIntegration.GetEntity<Processo>("/api/Processo", id);
        }

        public async Task<List<Processo>> GetProcesso()
        {
            Log.EscreverLog($"Chamou o método - Task<List<Processo>> GetProcesso()", Log.TipoLog.Trace);
            Log.EscreverLog("Consumindo ApiLegalScraper - Endpoint Get (/api/Processo)", Log.TipoLog.Trace);

            var result = await _apiLegalScraperIntegration.GetEntity<Processo>("/api/Processo");

            if (result != null)
            {
                Log.EscreverLog($"\n\n******************************************GET-LIST******************************************", Log.TipoLog.Trace);
                Log.EscreverLog($"Listas de Entidades 'Processo' recuperadas com sucesso!", Log.TipoLog.Trace);
                Log.EscreverLog($"Total de Entidades recuperadas {result.Count}.:", Log.TipoLog.Trace);
                Log.EscreverLog("--------------------------------------------------------", Log.TipoLog.Trace);
                foreach (var itemList in result)
                {
                    foreach (PropertyInfo propertyInfo in itemList.GetType().GetProperties())
                    {
                        Log.EscreverLog($"**{propertyInfo.Name}** --> '{propertyInfo.GetValue(itemList)}'", Log.TipoLog.Trace);
                    }
                    Log.EscreverLog("--------------------------------------------------------", Log.TipoLog.Trace);
                }
                
            }
            return result;
        }

        public async Task<bool> UpdateProcesso(Processo processo)
        {
            Log.EscreverLog($"Chamou o método - Task<bool> UpdateProcesso(Processo processo)", Log.TipoLog.Trace);
            Log.EscreverLog("Consumindo ApiLegalScraper - Endpoint Put (/api/Processo)", Log.TipoLog.Trace);

            processo.Numero = "".GeraNumeroPadraoCNJ();
            processo.Area = "Trabalhista";
            processo.Classe = "Habeas Corpus";
            processo.Distribuicao = "1a. Vara Criminal";
            processo.Origem = "Vara Criminal de Belo Horizonte";
            processo.Relator = "Marco Manhães Júnior";
            //processo.Relator = "Creciliane Naiara Dutra";

            var result = await _apiLegalScraperIntegration.UpdateEntity<Processo>("/api/Processo", processo);

            if (result)
            {
                Log.EscreverLog($"\n\n******************************************UPDATE******************************************", Log.TipoLog.Trace);
                Log.EscreverLog($"Entidade 'Processo' alterada com sucesso!", Log.TipoLog.Trace);
                Log.EscreverLog($"Entidade alterada:", Log.TipoLog.Trace);
                foreach (PropertyInfo propertyInfo in processo.GetType().GetProperties())
                {
                    Log.EscreverLog($"**{propertyInfo.Name}** --> '{propertyInfo.GetValue(processo)}'", Log.TipoLog.Trace);
                }
            }

            return result;
        }

        public async Task<Processo> CreateProcesso(Processo processo)
        {
            Log.EscreverLog($"Method call - Task<Processo> CreateProcesso(Processo processo)", Log.TipoLog.Trace);
            Log.EscreverLog("Consumindo ApiLegalScraper - Endpoint Post (/api/Processo)", Log.TipoLog.Trace);

            var result = await _apiLegalScraperIntegration.CreateEntity<Processo>("/api/Processo", processo);

            if (result != null)
            {
                Log.EscreverLog($"\n\n******************************************CREATE******************************************", Log.TipoLog.Trace);
                Log.EscreverLog($"Entidade 'Processo' salva com sucesso!", Log.TipoLog.Trace);
                Log.EscreverLog($"Valores so scraping realizado:", Log.TipoLog.Trace);
                foreach (PropertyInfo propertyInfo in result.GetType().GetProperties())
                {
                    Log.EscreverLog($"**{propertyInfo.Name}** --> '{propertyInfo.GetValue(result)}'", Log.TipoLog.Trace);
                }
            }
            return result;
        }

        public async Task<bool> DeleteProcesso(int id)
        {
            Log.EscreverLog($"Method call - Task<bool> DeleteProcesso(int id)", Log.TipoLog.Trace);
            Log.EscreverLog("Consumindo ApiLegalScraper - Endpoint Delete (/api/Processo)", Log.TipoLog.Trace);

            var result = await _apiLegalScraperIntegration.DeleteEntity<Processo>("/api/Processo", id);
            if (result)
            {
                Log.EscreverLog($"\n\n******************************************DELETE******************************************", Log.TipoLog.Trace);
                Log.EscreverLog($"Entidade 'Processo' excluída com sucesso!", Log.TipoLog.Trace);
                Log.EscreverLog($"Processo excluído: Id '{id}'", Log.TipoLog.Trace);
            }

            return result;
        }
    }
}
