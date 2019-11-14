using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteApp.Models.ViewModel;

namespace Infracoes.Models.ViewModel.Veiculos
{
    public class VeiculosCadastradosViewModel
    {
        public string Placa { get; set; }
        public PaginacaoViewModel Paginacao { get; private set; }
        public VeiculosCadastradosViewModel()
        {
            Paginacao = new PaginacaoViewModel();
        }
    }
}