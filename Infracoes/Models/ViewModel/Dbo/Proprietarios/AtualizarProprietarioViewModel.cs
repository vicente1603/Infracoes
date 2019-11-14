using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Infracoes.Models.ViewModel.Dbo.Proprietarios
{
    public class AtualizarProprietarioViewModel
    {
        [Required(ErrorMessage= "O Proprietário precisa ser informado.")]
        public int IdProprietario { get; set; }

        [Required(ErrorMessage = "O {0} precisa ser informado.")]
        [StringLength(50, ErrorMessage="Campo {0} deve conter no máximo {1} caracteres.")]
        public string NomeProprietario { get; set; }

        [Required(ErrorMessage = "O {0} precisa ser informado.")]
        [StringLength(50, ErrorMessage = "Campo {0} deve conter no máximo {1} caracteres.")]
        public string CpfProprietario { get; set; }

        [Required(ErrorMessage = "O {0} precisa ser informado.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O {0} precisa ser informado.")]
        [StringLength(50, ErrorMessage = "Campo {0} deve conter no máximo {1} caracteres.")]
        public string Telefone { get; set; }
    }
}