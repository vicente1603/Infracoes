using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Veiculos
{
    public class AtualizarVeiculoViewModel
    {
        [Required(ErrorMessage="{0} precisa ser informado.")]
        public int IdVeiculo { get; set; }
        
        [Required(ErrorMessage="{0} precisa ser informado.")]
        [StringLength(50, ErrorMessage= "Campo {0} deve conter no máximo {1} caracteres.")]
        public string Placa { get; set; }

        [Required(ErrorMessage="{0} precisa ser informado.")]
        [StringLength(2, ErrorMessage= "Campo {0} deve conter no máximo {1} caracteres.")]
        public string Uf { get; set; }
        public int IdInfracao { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public int IdModelo { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public int IdProprietario { get; set; }

    }
}