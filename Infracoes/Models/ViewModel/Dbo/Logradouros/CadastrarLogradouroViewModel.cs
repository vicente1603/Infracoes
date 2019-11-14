using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Logradouros
{
    public class CadastrarLogradouroViewModel
    {

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        [StringLength(50, ErrorMessage = "Campo {0} deverá conter no máximo {1} caracteres")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        [StringLength(50, ErrorMessage = "Campo {0} deverá conter no máximo {1} caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        [StringLength(50, ErrorMessage = "Campo {0} deverá conter no máximo {1} caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        [StringLength(50, ErrorMessage = "Campo {0} deverá conter no máximo {1} caracteres")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        [StringLength(50, ErrorMessage = "Campo {0} deverá conter no máximo {1} caracteres")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public double VelocidadeMax { get; set; }
    }
}