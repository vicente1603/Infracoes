using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Proprietarios
{
    public class ProprietariosCadastradosRelatorioViewModel
    {
        public string NomeProprietario { get; set; }
        public string CpfProprietario { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
    }
}