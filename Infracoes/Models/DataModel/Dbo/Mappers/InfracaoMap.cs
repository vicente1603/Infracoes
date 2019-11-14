using Infracoes.Models.DomainModel.Dbo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DataModel.Dbo.Mappers
{
    public class InfracaoMap : EntityTypeConfiguration<Infracao>
    {
        public InfracaoMap()
        {
            ToTable("Infracao","dbo");
            HasKey(i => i.IdInfracao);

            Property(v => v.IdInfracao).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(v => v.Velocidade).IsRequired();
            Property(v => v.IdVeiculo).IsRequired();
            Property(v => v.IdLogradouro).IsRequired();
            Property(v => v.IdAgente).IsRequired();
            Property(v => v.Descricao).IsRequired();

            HasRequired(i => i.Veiculo).WithMany(v => v.Infracoes).HasForeignKey(i => i.IdVeiculo);
            HasRequired(i => i.Agente).WithMany(v => v.Infracoes).HasForeignKey(i => i.IdAgente);
            HasRequired(i => i.Logradouro).WithMany(v => v.Infracoes).HasForeignKey(i => i.IdLogradouro);
        }
    }
}