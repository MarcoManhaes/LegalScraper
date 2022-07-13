using System;
using System.Collections.Generic;
using System.Text;

namespace LegalScraper.Domain.Configurations
{
    public class Configuration
    {

        public const string UrlApiBase = "https://localhost:44360";

        public const string UrlTribunal = "http://esaj.tjba.jus.br/cpo/sg/open.do";

        public const string RENumCNJ = @"^\d{7}-\d{2}.\d{4}.\d.\d{2}.\d{4}"; //@"^[0-9]{7}-[0-9]{2}.[0-9]{4}.[0-9].[0-9]{2}.[0-9]{4}"

        private List<string> _numerosProcessosMock = null;

        public List<string> NumerosProcessoMock
        {
            get
            {
                _numerosProcessosMock = new List<string>{ "0809979-67.2015.8.05.0080",
                        "0000825-06.2009.8.05.0036",
                        "0361076-17.2012.8.05.0001",
                        "0081392-76.2002.8.05.0001",
                        "0000004-44.2008.8.05.0098" };
                return _numerosProcessosMock;
            }
        }
            //"0809979-67.2015.8.05.0080:0000825-06.2009.8.05.0036:0361076-17.2012.8.05.0001:0081392-76.2002.8.05.0001:0000004-44.2008.8.05.0098";
    }
}

//0000825-06.2009.8.05.0036
//0361076 - 17.2012.8.05.0001
//0081392 - 76.2002.8.05.0001
//0000004 - 44.2008.8.05.0098