using Infracoes.Models.DomainModel.Dbo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DataModel.Dbo.Mappers
{
    public class ProprietarioMap : EntityTypeConfiguration<Proprietario>
    {
        public ProprietarioMap()
        {
            ToTable("Proprietario", "dbo");
            HasKey(p => p.IdProprietario);

            Property(p => p.IdProprietario).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.NomeProprietario).IsRequired();
            Property(p => p.CpfProprietario).IsRequired();
            Property(p => p.DataNascimento).IsRequired();
            Property(p => p.Telefone).HasMaxLength(50).IsRequired();

            HasMany(p => p.Veiculos).WithRequired(v => v.Proprietario).HasForeignKey(p => p.IdProprietario);
        }
    }
}