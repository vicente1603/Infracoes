using Infracoes.Models.DomainModel.Dbo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DataModel.Dbo.Mappers
{
    public class LogradouroMap : EntityTypeConfiguration<Logradouro>
    {
        public LogradouroMap()
        {
            ToTable("Logradouro", "dbo");
            HasKey(l => l.IdLogradouro);

            Property(l => l.IdLogradouro).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(l => l.Rua).HasMaxLength(50).IsRequired();
            Property(l => l.Bairro).HasMaxLength(50).IsRequired();
            Property(l => l.Cidade).HasMaxLength(50).IsRequired();
            Property(l => l.Estado).HasMaxLength(50).IsRequired();
            Property(l => l.Cep).HasMaxLength(50).IsRequired();
            Property(l => l.VelocidadeMax).IsRequired();

            HasMany(l => l.Infracoes).WithRequired(i => i.Logradouro).HasForeignKey(i => i.IdLogradouro);
        }
    }
}