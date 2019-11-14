using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteApp.Models.ViewModel;

namespace Infracoes.Models.ViewModel.Dbo.Agentes
{
    public class AgentesCadastradosViewModel
    {
        public string Matricula { get; set; }
        public PaginacaoViewModel Paginacao { get; private set; }
        public AgentesCadastradosViewModel()
        {
            Paginacao = new PaginacaoViewModel();
        }
    }
}