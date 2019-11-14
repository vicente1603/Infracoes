using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DomainModel.Dbo
{
    public class Proprietario
    {
        public int IdProprietario { get; set; }
        public string NomeProprietario { get; set; }
        public string CpfProprietario { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public virtual ICollection<Veiculo> Veiculos { get; set; }
    }
}