using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Modelo
{
    public class CadastrarModeloViewModel
    {
        [Required(ErrorMessage = "Campo '{0}' deve ser informado.")]
        [StringLength(50, ErrorMessage = "Campo '{0}' deve conter no máximo {1} caracteres.")]
        public string Descricao { get; set; }
    }
}