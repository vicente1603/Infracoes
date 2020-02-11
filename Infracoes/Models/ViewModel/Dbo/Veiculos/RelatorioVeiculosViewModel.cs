using Infracoes.Models.DomainModel.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Veiculos
{
    public class RelatorioVeiculosViewModel
    {
        public List<Veiculo> Veiculos { get; set; }
        public int TotalVeiculos { get; set; }

        public class Veiculo
        {
            public string Placa { get; set; }
            public string Uf { get; set; }
            public string Modelo { get; set; }
            public int Infracao { get; set; }
            public string Proprietario { get; set; }
        } 
    }
}