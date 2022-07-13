using ApiLegalScraper.Data.Repositories;
using ApiLegalScraper.Domain.Interfaces.Repositories;
using ApiLegalScraper.Domain.Interfaces.Services;
using ApiLegalScraper.Service.Services;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SQLite;
using LegalScraper.Data;
using LegalScraper.Data.Configurations;
using LegalScraper.Data.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace ApiLegalScraper.Controller
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
            ExecuteMigration();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            ConfigureDependencyInjection(services);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LegalScraperAPI", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LegalScraperAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        static void ExecuteMigration()
        {
            Console.WriteLine("Create the announcer to output the migration messages...");
            var announcer = new ConsoleAnnouncer();

            Console.WriteLine("Processor specific options (usually none are needed)...");
            var options = new ProcessorOptions();

            Console.WriteLine("Use SQLite...");
            var processorFactory = new SQLiteProcessorFactory();

            Console.WriteLine("Get ConnectionString...");
            var connectionString = ConnectionStringConfiguration.ObterConnectionString();

            Console.WriteLine("Initialize the processor...");
            using (var processor = processorFactory.Create(
                connectionString,
                announcer,
                options))
            {
                Console.WriteLine("Configure the runner...");
                var context = new RunnerContext(announcer);

                Console.WriteLine("Create the migration runner...");
                Console.WriteLine("Specify the assembly with the migrations...");
                var runner = new MigrationRunner(
                    typeof(Initial_Migration_1).Assembly,
                    context,
                    processor);

                Console.WriteLine("Run the migrations...");
                runner.MigrateUp();
            }
        }

        private static void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<ApplicationContext>();
            services.AddScoped<IProcessoRepository, ProcessoRepository>();
            services.AddScoped<IProcessoService, ProcessoService>();
            services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
            services.AddScoped<IMovimentacaoService, MovimentacaoService>();
        }
    }
}
