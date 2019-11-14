using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DomainModel.Dbo
{
    public class Agente
    {
        public int IdAgente { get; set; }
        public string NomeAgente { get; set; }
        public DateTime Efetivacao { get; set; }
        public int TempoServico { get; set; }
        public string Matricula { get; set; }
        public virtual ICollection<Infracao> Infracoes { get; set; }

    }
}