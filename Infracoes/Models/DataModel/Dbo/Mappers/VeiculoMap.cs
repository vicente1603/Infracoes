using Infracoes.Models.DomainModel.Dbo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Infracoes.Models.DataModel.Dbo.Mappers
{
    public class VeiculoMap : EntityTypeConfiguration<Veiculo>
    {
        public VeiculoMap()
        {
            ToTable("Veiculo", "dbo");
            HasKey(v => v.IdVeiculo);

            Property(v => v.IdVeiculo).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(v => v.Placa).IsRequired();
            Property(v => v.Uf).IsRequired();
            Property(v => v.IdInfracao).IsOptional();
            Property(v => v.IdModelo).IsRequired();
            Property(v => v.IdProprietario).IsRequired();

            HasMany(v => v.Infracoes).WithRequired(i => i.Veiculo).HasForeignKey(i => i.IdVeiculo);
            HasRequired(v => v.Proprietario).WithMany(p => p.Veiculos).HasForeignKey(v => v.IdProprietario);
            HasRequired(v => v.Modelo).WithMany(m => m.Veiculos).HasForeignKey(v => v.IdModelo);
        }
    }
}