using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.ViewModel.Dbo.Modelos
{
    public class RelatorioModelosViewModel
    {
        public List<Modelo> Modelos { get; set; }
        public int TotalModelos { get; set; }

        public class Modelo
        {
            public string Descricao { get; set; }
        }
    }
}