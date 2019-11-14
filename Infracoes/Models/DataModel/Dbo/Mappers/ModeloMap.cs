using Infracoes.Models.DomainModel.Dbo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DataModel.Dbo.Mappers
{
    public class ModeloMap : EntityTypeConfiguration<Modelo>
    {
        public ModeloMap()
        {
            ToTable("Modelo", "dbo");
            HasKey(m => m.IdModelo);

            Property(m => m.IdModelo).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Descricao).HasMaxLength(50).IsRequired();

            HasMany(m => m.Veiculos).WithRequired(v => v.Modelo).HasForeignKey(v => v.IdModelo);
        }
    }
}