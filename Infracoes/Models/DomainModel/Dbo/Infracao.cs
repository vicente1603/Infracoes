using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DomainModel.Dbo
{
    public class Infracao
    {
        public int IdInfracao { get; set; }
        public double Velocidade { get; set; }
        public int IdVeiculo { get; set; }
        public int IdLogradouro { get; set; }
        public int IdAgente { get; set; }
        public string Descricao { get; set; }

        public virtual Veiculo Veiculo { get; set; }
        public virtual Logradouro Logradouro { get; set; }
        public virtual Agente Agente { get; set; }



    }
}