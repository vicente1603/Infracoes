using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteApp.Models.ViewModel;

namespace Infracoes.Models.ViewModel.Dbo.Modelo
{
    public class ModelosCadastradosViewModel
    {
        public string Descricao { get; set; }

        public PaginacaoViewModel Paginacao { get; private set; }

        public ModelosCadastradosViewModel()
        {
            Paginacao = new PaginacaoViewModel();
        }
    }
}