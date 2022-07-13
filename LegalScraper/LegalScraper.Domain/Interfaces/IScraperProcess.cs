using LegalScraper.Domain.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LegalScraper.Domain.Interfaces
{
    public interface IScraperProcess
    {
        IWebDriver ExecuteStep1CrawlingProcess();
        IWebDriver ExecuteStep2CrawlingProcess(IWebDriver driver, string numProcesso);
        Processo ExecuteStep1ScrapingProcess(IWebDriver driver);
        List<Movimentacao> ExecuteStep2ScrapingProcess(IWebDriver driver);
        Processo DeParaDadosProcesso(Dictionary<string, string> dadosProcesso);
    }
}
