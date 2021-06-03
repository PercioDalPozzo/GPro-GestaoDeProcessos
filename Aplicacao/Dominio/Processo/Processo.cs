using System;
using System.Collections.Generic;

namespace Aplicacao.Dominio.Responsavel
{
    public class Processo
    {
        public Processo()
        {
            ProcessoResponsavel = new List<ProcessoResponsavel>();
        }

        public int Id { get; set; }
        public string NumeroProcesso { get; set; }
        public DateTime DataDistribuicao { get; set; }

        public List<ProcessoResponsavel> ProcessoResponsavel { get; set; }
    }
}
