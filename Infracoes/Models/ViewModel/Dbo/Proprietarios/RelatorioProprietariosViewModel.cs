using Infracoes.Models.DomainModel.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Proprietarios
{
    public class RelatorioProprietariosViewModel
    {
        public List<Proprietario> Proprietarios { get; set; }
        public int TotalProprietarios { get; set; }

        public class Proprietario
        {
            public string NomeProprietario { get; set; }
            public string CpfProprietario { get; set; }
            public string DataNascimento { get; set; }
            public string Telefone { get; set; }
        }
    }
}