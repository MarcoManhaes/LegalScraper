
using ApiLegalCrawler.Domain.Interfaces.Repositories;
using ApiLegalCrawler.Data.Repositories;
using LegalCrawler.Data;
using System;
using Unity;
using Unity.Resolution;
using ApiLegalCrawler.Service.Services;
using ApiLegalCrawler.Domain.Interfaces.Services;

namespace LegalCrawler.Ioc
{
    public static class DependencyInjection
    {
        private static IUnityContainer _container;


        public static void Configure()
        {
            if (_container == null)
                _container = new UnityContainer();

            _container.RegisterInstance<ApplicationContext>(new ApplicationContext());
            _container.RegisterType<IProcessoService, ProcessoService>();
            _container.RegisterType<IProcessoRepository, ProcessoRepository>();
            


            //_container.RegisterType<Imagem>();
            //_container.RegisterType<ImagemNotaFiscalServico>();
            //_container.RegisterType<ImagemNotaFiscalProduto>();
            //_container.RegisterType<ImagemBoleto>();
            //_container.RegisterType<ImagemBoletoConcessionaria>();

            //_container.RegisterType<ExtrairDadosNotaFiscalServico>();
            //_container.RegisterType<ExtrairDadosNotaFiscalProduto>();
            //_container.RegisterType<ExtrairDadosBoleto>();
            //_container.RegisterType<ExtrairDadosBoletoConcessionaria>();


        }

        public static T Resolve<T>()
        {
            if (_container == null)
                _container = new UnityContainer();
            return _container.Resolve<T>();
        }

        public static T Resolve<T>(ResolverOverride[] param = null)
        {
            try
            {
                if (_container == null)
                    _container = new UnityContainer();
                return _container.Resolve<T>(param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
