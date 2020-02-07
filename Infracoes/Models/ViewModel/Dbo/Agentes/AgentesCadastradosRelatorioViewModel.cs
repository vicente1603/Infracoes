using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Agentes
{
    public class AgentesCadastradosRelatorioViewModel
    {
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public string DataEfetivacao { get; set; }
        public string TempoServico { get; set; }
    }
}