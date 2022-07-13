using LegalCrawler.Ioc;
using LegalScraper.Domain.Configurations;
using LegalScraper.Domain.Interfaces;
using LegalScraper.Domain.Models;
using LegalScraper.Service.Integrations;
using LegalScraper.Service.Scraping;
using LegalScraper.Service.Services;
using LegalScraper.Utility;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LegalScraper
{
    class Program
    {
        async static Task Main(string[] args)
        {       
            IWebDriver driver = null;
            try
            {
                var services = new ServiceCollection();
                Log.EscreverLog($"Iniciando a aplicação 'Legal Scraper'...", Log.TipoLog.Trace);
                Log.EscreverLog($"Configurando injeção de dependências...", Log.TipoLog.Trace);

                DependencyInjection.Configure();

                IScraperProcess scraper = new ScraperProcess(
                    new ApiLegalScraperIntegration());

                IProcessoIntegration integrationProcesso = new ProcessoIntegration(
                    new ApiLegalScraperIntegration());
                IMovimentacaoIntegration integrationMovimentacao = new MovimentacaoIntegration(
                    new ApiLegalScraperIntegration());

                driver = scraper.ExecuteStep1CrawlingProcess();

                Log.EscreverLog($"^\n\n==================================================================================================================", Log.TipoLog.Trace);
                Log.EscreverLog($"No fluxo a seguir, a aplicação irá reralizar o rastreamento e raspagem de dados\n" +
                                $"de 5 processos distintos e posteriormente manipulá-los em base de dados local.", Log.TipoLog.Trace);

                foreach (var numProcessoMock in new Configuration().NumerosProcessoMock)
                {
                    scraper.ExecuteStep2CrawlingProcess(driver, numProcessoMock);
                    var processo = scraper.ExecuteStep1ScrapingProcess(driver);
                    Log.EscreverLog($"\n==================================================================================================================", Log.TipoLog.Trace);
                    Log.EscreverLog($"Validação processo numero: {numProcessoMock}\n", Log.TipoLog.Trace);

                    var nProcesso = await CreateProcesso(integrationProcesso, processo);

                    //TODO log movimentacoes (diminuir tamanho logs)
                    var movimentacoes = scraper.ExecuteStep2ScrapingProcess(driver);
                    await CreateMovimentacoes(integrationMovimentacao, movimentacoes, nProcesso);
                }

                await ValidateProcesso(integrationProcesso);


                driver.Quit();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Log.EscreverLog($"Ocorreu um erro inesperado. Entre em contato com o administrador.", Log.TipoLog.Trace, ex.Message);
                
                if(driver != null) 
                    driver.Quit();

                Environment.Exit(0);
            }           
        }

        private static async Task ValidateProcesso(IProcessoIntegration integration)
        {
            Log.EscreverLog($"^\n\n==================================================================================================================", Log.TipoLog.Trace);
            Log.EscreverLog($"No fluxo a seguir, a aplicação irá consumir a api GET(LIST) para buscar a lista \n" +
                            $"dos dados após extraídos e persistidos na base de dados", Log.TipoLog.Trace);
            var processoList = await integration.GetProcesso();

            Log.EscreverLog($"^\n\n==================================================================================================================", Log.TipoLog.Trace);
            Log.EscreverLog($"No fluxo a seguir, a aplicação irá consumir a api GET para buscar um geristro específico, \n" +
                            $"de acordo com o id informado no filtro. Este registro será utilizado no fluxo de update a seguir.", Log.TipoLog.Trace);
            var processoLocal = await integration.GetProcesso((int)processoList[processoList.Count - 1].Id);

            Log.EscreverLog($"^\n\n==================================================================================================================", Log.TipoLog.Trace);
            Log.EscreverLog($"No fluxo a seguir, a aplicação irá consumir a api UPDATE para persistência dos dados\n" +
                            $"atualizados para o registro filtrado na busca anterior (GET)", Log.TipoLog.Trace);
            var updated = await integration.UpdateProcesso(processoLocal);

            Log.EscreverLog($"^\n\n==================================================================================================================", Log.TipoLog.Trace);
            Log.EscreverLog($"No fluxo a seguir, a aplicação irá consumir a api GET(LIST) para buscar a lista \n" +
                            $"de dados e selecionar um para realizar a exclusão do mesmo através do endpoint de DELETE", Log.TipoLog.Trace);
            var processoListDelete = await integration.GetProcesso();
            if (processoListDelete.Count > 0)
                await integration.DeleteProcesso((int)processoListDelete[processoListDelete.Count - 1].Id);

            Log.EscreverLog($"^\n\n==================================================================================================================", Log.TipoLog.Trace);
            Log.EscreverLog($"No fluxo a seguir, a aplicação irá consumir a api GET(LIST) para buscar a lista \n" +
                            $"dos dados na qual não consta mais o registro excluído", Log.TipoLog.Trace);
            var processoFinalList = await integration.GetProcesso();
        }

        private static async Task<Processo> CreateProcesso(IProcessoIntegration integration, Processo processo)
        {
            Log.EscreverLog($"Chamou o método - Task<Processo> CreateProcesso(IProcessoIntegration integration, Processo processo)", Log.TipoLog.Trace);

            Log.EscreverLog($"^\n\n==================================================================================================================", Log.TipoLog.Trace);
            Log.EscreverLog($"No fluxo a seguir, a aplicação irá consumir a api CREATE para persistência dos dados\n" +
                            $"extraídos do site do tribunal de justiça da Bahía detalhando-o neste logcriação ", Log.TipoLog.Trace);
            var nProcesso = await integration.CreateProcesso(processo);
            return nProcesso;
        }

        private static async Task CreateMovimentacoes(IMovimentacaoIntegration integration, List<Movimentacao> movimentacoes, Processo processo)
        {
            foreach (var m in movimentacoes)
                m.ProcessoId = (int)processo.Id;

            var nProcesso = await integration.CreateMovimentacao(movimentacoes);
        }
    }
}
