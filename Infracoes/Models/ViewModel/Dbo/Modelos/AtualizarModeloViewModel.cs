using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Modelo
{
    public class AtualizarModeloViewModel
    {
        [Required(ErrorMessage = "Modelo precisa ser informado.")]
        public int IdModelo { get; set; }

        [StringLength(50, ErrorMessage = "Campo {0} deve ser informado")]
        public string Descricao { get; set; }
    }
}