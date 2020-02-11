using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Infracoes
{
    public class InfracoesCadastradasRelatorioViewModel
    {
        public int IdInfracao { get; set; }
        public double Velocidade { get; set; }
        public int IdVeiculo { get; set; }
        public int IdLogradouro { get; set; }
        public int IdAgente { get; set; }
        public string Descricao { get; set; }
    }
}