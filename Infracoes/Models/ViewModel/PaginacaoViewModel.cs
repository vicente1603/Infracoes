using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteApp.Models.ViewModel
{
    public class PaginacaoViewModel
    {
        public int Limite { get; set; }
        public int Inicio
        {
            get { return (Pagina - 1) * Limite; }
        }
        public int Pagina { get; set; }
        public int TotalPaginas
        {
            get
            {
                return Convert.ToInt32(Math.Ceiling((Double)TotalRegistros / (Double)Limite));
            }
        }
        public int TotalRegistros { get; set; }

        public PaginacaoViewModel()
        {
            Pagina = 1;
            Limite = 10;
        }

        public object Json()
        {
            return new
            {
                limite = Limite,
                pagina = Pagina,
                totalPaginas = TotalPaginas,
                registros = TotalRegistros
            };
        }
    }
}
