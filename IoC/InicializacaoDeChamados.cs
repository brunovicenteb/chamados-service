using System.Reflection;
using Chamados.Service.Infra.Data;
using Chamados.Service.Application;
using Chamados.Service.Domain.Interfaces.Repositorios;
using Chamados.Service.Domain.Interfaces.Servicos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;

namespace Chamados.Service.IoC;

public static class InicializacaoDeChamados
{
    public static void ConfiguraServicoDeChamados(this IServiceCollection servico)
    {
        servico.AddScoped<IChamadosRepositorio, ChamadosRepositorio>();
        servico.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", CriaInformacoesDaApi());
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opt.IncludeXmlComments(xmlPath);
        });
        RegistraServico(servico);
    }

    private static void RegistraServico(IServiceCollection servico)
    {
        servico.AddScoped<IChamadosServico, ChamadosServico>();
    }

    public static void ConfiguraChamados(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoodeshAPI v1"));
    }

    private static OpenApiInfo CriaInformacoesDaApi()
    {
        return new OpenApiInfo
        {
            Title = "Coodesh Back-end Challenge API",
            Version = "v1",
            Description = "This is a REST API that will use data from the Space Flight News project, " +
                   "a public API with information related to spaceflight. " +
                   "The project was created so that Coodesh has practical conditions to assess the skills " +
                   "of candidate Bruno Belchior for the vacancy of Back-end Developer.",
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