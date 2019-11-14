using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DomainModel.Dbo
{
    public class Veiculo
    {
        public int IdVeiculo { get; set; }
        public string Placa { get; set; }
        public string Uf { get; set; }
        public Nullable<int> IdInfracao { get; set; }
        public int IdModelo { get; set; }
        public int IdProprietario { get; set; }

        public virtual ICollection<Infracao> Infracoes { get; set; }
        public virtual Proprietario Proprietario { get; set; }
        public virtual Modelo Modelo { get; set; }

    }
}