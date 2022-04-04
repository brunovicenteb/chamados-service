using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Chamados.Service.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Chamados.Service.Infra.Data.Postgres;
using Microsoft.Extensions.DependencyInjection;
using Chamados.Service.Domain.Interfaces.Servicos;
using Chamados.Service.Infra.Data.Mongo.Mapeamentos;
using Chamados.Service.Domain.Interfaces.Repositorios;
using Chamados.Service.Domain.Entidades;
using Chamados.Service.Domain.Modelos;

namespace Chamados.Service.IoC;

public static class InicializacaoDeChamados
{
    public static void ConfiguraServicoDeChamados(this IServiceCollection servicos, IConfiguration configuracoes)
    {
        var stringDeConexao = configuracoes.GetValue<string>("PostgreSettings:ConnectionString");
        servicos.AddDbContext<ContextoPostgres>(o => o.UseNpgsql(stringDeConexao), ServiceLifetime.Transient);
        servicos.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", CriaInformacoesDaApi());
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opt.IncludeXmlComments(xmlPath);
            xmlFile = $"Chamados.Service.Domain.xml";
            xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opt.IncludeXmlComments(xmlPath);
        });
        RegistraServico(servicos);
    }

    private static void RegistraServico(IServiceCollection servicos)
    {
        MapeamentoChamados.Mapear();
        servicos.AddScoped<IServico<Chamado, string, NovoChamado, ChamadoAlterado>, ServicoChamado>();
        servicos.AddScoped<IRepositorio<Chamado, string>, RepositorioChamadoPostgres>();
    }

    public static void ConfiguraChamados(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chamados-Service v1"));
    }

    private static OpenApiInfo CriaInformacoesDaApi()
    {
        return new OpenApiInfo
        {
            Title = "Chamados-Service API",
            Version = "v1",
            Description = "Essa é uma Api de fins didáticos, criada para controle de chamados de suporte de uma empresa fictícia. " +
                   "Seu intuito é apresentar uma visão clara de um projeto desenvolvido " +
                   "utilizando as práticas de Clean Architecture e servir como exemplo de consulta  " +
                   "para novos projetos similares.",
            Contact = new OpenApiContact
            {
                Name = "Bruno Belchior",
                Email = "brunovicenteb@gmail.com",
                Url = new Uri("https://github.com/brunovicenteb")
            },
            License = new OpenApiLicense
            {
                Name = "MIT",
                Url = new Uri("https://opensource.org/licenses/MIT")
            },
            TermsOfService = new Uri("https://opensource.org/licenses/MIT")
        };
    }
}