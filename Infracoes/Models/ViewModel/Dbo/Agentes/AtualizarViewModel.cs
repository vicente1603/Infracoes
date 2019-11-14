using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Agentes
{
    public class AtualizarViewModel
    {
        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public int IdAgente { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        [StringLength(50, ErrorMessage ="Campo {0} deverá conter no máximo {1} caracteres")]
        public string NomeAgente { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public DateTime Efetivacao { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public int TempoServico { get; set; }

        [Required(ErrorMessage = "{0} precisa ser informado.")]
        public string Matricula { get; set; }

    }
}