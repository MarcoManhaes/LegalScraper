using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LegalScraper.Utility
{
    public static class Log
    {
        private const string _log_name = "LegalScraper";
        public static string CaminhoArquivoLog
        {
            get
            {
                var caminho = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Logs");
                if (!Directory.Exists(caminho))
                {
                    Directory.CreateDirectory(caminho);
                }

                return caminho;
            }
        }

        public static string GetLogName => _log_name;

        private static LoggingConfiguration loggingConfiguration = null;
        public static void ConfigurarLog()
        {
            if (loggingConfiguration == null)
            {
                string nomeArquivoLog = string.Format("{0:yyyyMMdd}_{1}.log", DateTime.Now, GetLogName);
                string caminhoArquivoLog = string.Format(@"{0}\{1}", CaminhoArquivoLog, nomeArquivoLog);

                loggingConfiguration = new LoggingConfiguration();
                var logfile = new FileTarget("logfile") { FileName = caminhoArquivoLog, Layout = "${longdate}|(${machinename} - ${hostname})|${threadname}|${level:uppercase=true} => ${message}${exception:format=@}" };
                loggingConfiguration.AddRule(LogLevel.Trace, LogLevel.Fatal, logfile);
            }
            LogManager.Configuration = loggingConfiguration;
        }

        public static void EscreverLog(string mensagem, TipoLog tipoLog, string logDetalhado = null)
        {
            ConfigurarLog();
            Logger logger = LogManager.GetCurrentClassLogger();


            if (logDetalhado != null)
                mensagem += $": {logDetalhado}";

            switch (tipoLog)
            {
                case TipoLog.Error:
                    logger.Error(mensagem);
                    break;
                case TipoLog.Trace:
                    logger.Trace(mensagem);
                    break;
                default:
                    logger.Info(mensagem);
                    break;
            }
        }

        public static void EscreverLog(string mensagem, Exception exception)
        {
            ConfigurarLog();
            Logger logger = LogManager.GetCurrentClassLogger();

            logger.Error(exception, mensagem);
        }

        public enum TipoLog
        {
            [Description("SUCCESS")]
            Suscess = 0,
            [Description("ERROR")]
            Error = 1,
            [Description("WARNING")]
            Warning = 2,
            [Description("TRACE")]
            Trace = 3
        }

        public static void Shutdown()
        {
            LogManager.Shutdown();
        }
    }
}
