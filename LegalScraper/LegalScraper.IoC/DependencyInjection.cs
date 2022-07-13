using LegalScraper.Domain.Interfaces;
using LegalScraper.Service.Integrations;
using LegalScraper.Service.Scraping;
using LegalScraper.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Resolution;

namespace LegalCrawler.Ioc
{
    public static class DependencyInjection
    {
        private static IUnityContainer _container;


        public static void Configure()
        {
            if (_container == null)
                _container = new UnityContainer();

            _container.RegisterType<ProcessoIntegration>();
            _container.RegisterType<MovimentacaoIntegration>();
            _container.RegisterType<IProcessoIntegration, ProcessoIntegration>();
            _container.RegisterType<ApiLegalScraperIntegrationBase, ApiLegalScraperIntegration>();
            _container.RegisterType<IScraperProcess, ScraperProcess>();
            _container.RegisterType<IScraperProcess, ScraperProcess>();

        }
    }
}
