using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Infracoes.Models.DomainModel.Dbo;
using Infracoes.Models.DataModel.Dbo.Mappers;

namespace Infracoes.Models.DataModel
{
    public class DbApplication : DbContext
    {
        public DbSet<Agente> Agentes { get; set; }
        public DbSet<Infracao> Infracoes { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Proprietario> Proprietarios { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }

        public DbApplication()
            : base("name=Infracoes")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new AgenteMap());
            modelBuilder.Configurations.Add(new InfracaoMap());
            modelBuilder.Configurations.Add(new LogradouroMap());
            modelBuilder.Configurations.Add(new ModeloMap());
            modelBuilder.Configurations.Add(new ProprietarioMap());
            modelBuilder.Configurations.Add(new VeiculoMap());

        }

        public void RegistrarNovo(object entidade)
        {
            Set(entidade.GetType()).Add(entidade);
        }

        public void RegistrarAlterado(object entidade)
        {
            Entry(entidade).State = EntityState.Modified;
        }
        public void RegistrarRemovido(object entidade)
        {
            Entry(entidade).State = EntityState.Deleted;
        }
        public void Salvar()
        {
            SaveChanges();
        }
    }
}