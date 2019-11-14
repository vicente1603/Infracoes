using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteApp.Models.ViewModel;

namespace Infracoes.Models.ViewModel.Dbo.Proprietarios
{
    public class ProprietariosCadastradosViewModel
    {
        public string CpfProprietario { get; set; }
        public string NomeProprietario { get; set; }

        public DateTime DataPedido { get; set; }
        
        public PaginacaoViewModel Paginacao { get; private set; }

        public ProprietariosCadastradosViewModel()
        {
            Paginacao = new PaginacaoViewModel();
        }
    }
}