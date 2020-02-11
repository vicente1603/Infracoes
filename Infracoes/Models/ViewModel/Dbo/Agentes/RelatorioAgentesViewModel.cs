using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Agentes
{
    public class RelatorioAgentesViewModel
    {
        public List<Agente> Agentes { get; set; }
        public int TotalAgentes { get; set; }

        public class Agente
        {
            public string Nome { get; set; }
            public string Matricula { get; set; }
            public string DataEfetivacao { get; set; }
            public int TempoServico { get; set; }
        }   
    }
}