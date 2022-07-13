using LegalScraper.Data.Configurations;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using ApiLegalScraper.Domain.Models;
using System.Data.Entity.Core.Common;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite.EF6;

namespace LegalScraper.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base(CreateConnection(), true)
        {
            DbConfiguration.SetConfiguration(new SQLiteConfiguration());
            Database.SetInitializer<ApplicationContext>(null);
        }

        private static DbConnection CreateConnection()
        {
            string connectionString = ConnectionStringConfiguration.ObterConnectionString();

            return new SQLiteConnection(connectionString);
        }

        public class SQLiteConfiguration : DbConfiguration
        {
            public SQLiteConfiguration()
            {
                SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
                SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
                SetProviderServices("System.Data.SQLite", (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
            }
        }

        public bool Commit()
        {
            try
            {
                lock (this)
                {
                    return this.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DbSet<Processo> Processos { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.Configuration.LazyLoadingEnabled = false;

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Processo>()
                .HasMany<Movimentacao>(x => x.Movimentacoes)
                .WithRequired(s => s.Processo)
                .HasForeignKey<int>(s => s.ProcessoId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Movimentacao>()
                .HasRequired(x => x.Processo)
                .WithRequiredPrincipal();
        }
    }
}
