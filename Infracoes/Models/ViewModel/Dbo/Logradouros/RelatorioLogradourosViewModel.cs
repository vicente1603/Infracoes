using Infracoes.Models.DomainModel.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Logradouros
{
    public class RelatorioLogradourosViewModel
    {
        public List<Logradouro> Logradouros { get; set; }
        public int TotalLogradouros { get; set; }

        public class Logradouro
        {
            public string Rua { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }
            public string Cep { get; set; }
            public double VelocidadeMax { get; set; }
        }
    }
}