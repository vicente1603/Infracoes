using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DomainModel.Dbo
{
    public class Modelo
    {
        public int IdModelo { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Veiculo> Veiculos { get; set; }
    }
}