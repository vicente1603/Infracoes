using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Agentes
{
    public class IdAgenteViewModel
    {
        [Required(ErrorMessage = "Campo {0} é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Campo {0} deve ser maior que 0")]

        public int IdAgente { get; set; }
    }
}