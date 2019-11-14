using Infracoes.Models.DomainModel.Dbo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DataModel.Dbo.Mappers
{
    public class AgenteMap : EntityTypeConfiguration<Agente>
    {
        public AgenteMap()
        {
            ToTable("Agente", "dbo");
            HasKey(a => a.IdAgente);

            Property(a => a.IdAgente).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(a => a.NomeAgente);
            Property(a => a.TempoServico).IsRequired();
            Property(a => a.Efetivacao).IsRequired();
            Property(a => a.Matricula).IsRequired();

            HasMany(a => a.Infracoes).WithRequired(i => i.Agente).HasForeignKey(i => i.IdAgente);
        }
    }
}