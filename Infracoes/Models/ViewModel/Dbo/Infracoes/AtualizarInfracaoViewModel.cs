using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Infracoes
{
    public class AtualizarInfracaoViewModel
    {

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public int IdInfracao { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        [Range(1, double.MaxValue, ErrorMessage = "Campo '{0}' dever ser maior que '0'")]
        public double Velocidade { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public int IdVeiculo { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public int IdLogradouro { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public int IdAgente { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        [StringLength(50, ErrorMessage= "O campo {0} deve ter no máximo {1} caracteres.")]
        public string Descricao { get; set; }
    }
}