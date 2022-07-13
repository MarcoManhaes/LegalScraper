using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLegalScraper.Domain.Models
{
    public class Movimentacao
    {
        public int? Id { get; set; }
        public int ProcessoId { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public Processo Processo { get; set; }
    }
}
