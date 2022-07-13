using LegalScraper.Domain.Configurations;
using LegalScraper.Domain.Interfaces;
using LegalScraper.Domain.Models;
using LegalScraper.Service.Integrations;
using LegalScraper.Utility;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LegalScraper.Service.Scraping
{
    public class ScraperProcess : IScraperProcess
    {
        private readonly ApiLegalScraperIntegrationBase _apiLegalScraperIntegration;
        
        public ScraperProcess(ApiLegalScraperIntegrationBase apiLegalScraperIntegration)
        {
            _apiLegalScraperIntegration = apiLegalScraperIntegration;
        }

        public IWebDriver ExecuteStep1CrawlingProcess()
        {
            try
            {              
                Log.EscreverLog($"Chamou o método - IWebDriver ExecuteStep1CrawlingProcess()", Log.TipoLog.Trace);
                IWebDriver driver = new ChromeDriver();

                driver.Navigate().GoToUrl(Configuration.UrlTribunal);
                var elementRadio = driver.FindElement(By.XPath("//*[@id=\"radioNumeroAntigo\"]"));
                elementRadio.Click();

                return driver;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public IWebDriver ExecuteStep2CrawlingProcess(IWebDriver driver, string numProcesso)
        {
            try
            {
                Log.EscreverLog($"Chamou o método - IWebDriver ExecuteStep2CrawlingProcess(string numProcesso)", Log.TipoLog.Trace);

                var elementNumProcesso = driver.FindElement(By.XPath("//*[@id=\"nuProcessoAntigoFormatado\"]"));
                
                elementNumProcesso.Clear();
                elementNumProcesso.SendKeys(numProcesso);

                var elementPesquisar = driver.FindElement(By.XPath("//*[@id=\"botaoPesaquisar\"]"));
                elementPesquisar.Submit();

                return driver;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Processo ExecuteStep1ScrapingProcess(IWebDriver driver)
        {
            try
            {
                List<Task> TaskList = new List<Task>();
                Log.EscreverLog($"Chamou o método - Processo ExecuteStep1ScrapingProcess(IWebDriver driver)", Log.TipoLog.Trace);

                var dadosProcesso = ScrapeProcesso(driver);
                //var movimentacoes = ScrapeMovimentacoes(driver);

                var processo = DeParaDadosProcesso(dadosProcesso);
                //processo.Movimentacoes = movimentacoes;
                return processo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Movimentacao> ExecuteStep2ScrapingProcess(IWebDriver driver)
        {
            try
            {
                List<Task> TaskList = new List<Task>();
                Log.EscreverLog($"Chamou o método - List<Movimentacao> ExecuteStep2ScrapingProcess(IWebDriver driver)", Log.TipoLog.Trace);

                //var dadosProcesso = ScrapeProcesso(driver);
                var movimentacoes = ScrapeMovimentacoes(driver);

                //var processo = DeParaDadosProcesso(dadosProcesso);
                //processo.Movimentacoes = movimentacoes;
                return movimentacoes;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private Dictionary<string, string> ScrapeProcesso(IWebDriver driver)
        {
            try
            {
                Log.EscreverLog($"Chamou o método - Dictionary<string, string> ScrapeProcesso(IWebDriver driver)", Log.TipoLog.Trace);

                var linhasProcesso = driver.FindElements(By.XPath("/html/body/table[4]/tbody/tr/td/table[2]/tbody/tr/td/div"));
                Dictionary<string, string> dadosProcesso = new Dictionary<string, string>();

                for (int i = 1; i <= linhasProcesso.Count; i++)
                {
                    var titulo = driver.FindElement(By.XPath($"/html/body/table[4]/tbody/tr/td/table[2]/tbody/tr[{i}]/td[1]")).Text;
                    var valor = driver.FindElement(By.XPath($"/html/body/table[4]/tbody/tr/td/table[2]/tbody/tr[{i}]/td[2]")).Text;

                    string[] splitValue = valor.Split(":");
                    if (splitValue.Length > 1)
                    {
                        var t = splitValue[0].Replace(":", "").RemoveAcentos();
                        if (dadosProcesso.ContainsKey(t))
                            continue;

                        dadosProcesso.Add(splitValue[0].Replace(":", "").RemoveAcentos(), splitValue[1]);
                    }
                    else
                        dadosProcesso.Add(titulo.Replace(":", "").Replace("Processo", "Numero").RemoveAcentos(), valor);


                    dadosProcesso["Numero"] = Regex.Match(dadosProcesso["Numero"], Configuration.RENumCNJ).Value;
                }

                return dadosProcesso;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<Movimentacao> ScrapeMovimentacoes(IWebDriver driver)
        {
            try
            {
                Log.EscreverLog($"Chamou o método - ExecuteScrapingProcess(IWebDriver driver)", Log.TipoLog.Trace);

                var linhasMovimentacoes = driver.FindElements(By.XPath("//*[@id=\"tabelaUltimasMovimentacoes\"]/tbody/tr/td[3]"));

                List<Movimentacao> movimentacoes = new List<Movimentacao>();

                for (int i = 1; i <= linhasMovimentacoes.Count; i++)
                {
                    var data = driver.FindElement(By.XPath($"//*[@id=\"tabelaUltimasMovimentacoes\"]/tbody/tr[{i}]/td[1]")).Text;
                    var descricao = driver.FindElement(By.XPath($"//*[@id=\"tabelaUltimasMovimentacoes\"]/tbody/tr[{i}]/td[3]")).Text;

                    var movimentacao = new Movimentacao();
                    movimentacao.Data = Convert.ToDateTime(data.ToString()).Date;
                    movimentacao.Descricao = descricao;

                    movimentacoes.Add(movimentacao);
                }

                return movimentacoes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Processo DeParaDadosProcesso(Dictionary<string, string> dadosProcesso)
        {
            try
            {
                Log.EscreverLog($"Chamou o método - Processo DeParaDadosProcesso(Dictionary<string, string> dadosProcesso)", Log.TipoLog.Trace);

                var processo = new Processo();
                foreach (PropertyInfo propertyInfo in processo.GetType().GetProperties())
                {
                    if (!dadosProcesso.ContainsKey(propertyInfo.Name)) continue;
                    propertyInfo.SetValue(processo, dadosProcesso[propertyInfo.Name]);
                }

                return processo;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
