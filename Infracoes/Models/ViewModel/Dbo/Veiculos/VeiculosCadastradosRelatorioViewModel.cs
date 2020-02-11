using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Veiculos
{
    public class VeiculosCadastradosRelatorioViewModel
    {
        public string Placa { get; set; }
        public string Uf { get; set; }
        public int IdInfracao { get; set; }
        public int IdModelo { get; set; }
        public int IdProprietario { get; set; }
    }
}