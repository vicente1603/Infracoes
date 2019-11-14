using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteApp.Models.ViewModel;

namespace Infracoes.Models.ViewModel.Dbo.Logradouros
{
    public class LogradourosCadastradosViewModel
    {
        public string Cep { get; set; }

        public PaginacaoViewModel Paginacao { get; private set; }

        public LogradourosCadastradosViewModel()
        {
            Paginacao = new PaginacaoViewModel();
        }
    }
}