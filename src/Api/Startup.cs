using Chamados.Service.IoC;

namespace Chamados.Service.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection pServices)
        {
            pServices.AddControllers();
            pServices.ConfiguraServicoDeChamados();
        }

        public void Configure(IApplicationBuilder pApp, IWebHostEnvironment pEnv, ILoggerFactory pLog)
        {
            pApp.UseExceptionHandler("/error");
            if (pEnv.IsDevelopment())
                pApp.UseDeveloperExceptionPage();
            pApp.UseRouting();
            pApp.ConfiguraChamados(pEnv);
            var logger = pLog.CreateLogger<Startup>();
            logger.LogInformation("#############################################################");
            logger.LogInformation("###              Executando Configurações                 ###");
            logger.LogInformation("#############################################################");
        }
    }
}