using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLegalScraper.Domain.Models
{
    public class Processo
    {
        public int? Id { get; set; }
        public string Numero { get; set; }
        public string Classe { get; set; }
        public string Area { get; set; }
        public string Origem { get; set; }
        public string Distribuicao { get; set; }
        public string Relator { get; set; }
        public List<Movimentacao> Movimentacoes { get; set; }
    }
}
