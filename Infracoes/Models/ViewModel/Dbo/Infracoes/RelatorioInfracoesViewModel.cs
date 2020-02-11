using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Infracoes
{
    public class RelatorioInfracoesViewModel
    {
        public List<Infracao> Infracoes { get; set; }
        public int TotalInfracoes { get; set; }

        public class Infracao
        {
            public double Velocidade { get; set; }
            public string Veiculo { get; set; }
            public string Logradouro { get; set; }
            public string Agente { get; set; }
            public string Descricao { get; set; }
        }
    }
}