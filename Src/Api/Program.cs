using Serilog;
using Chamados.Service.IoC;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfiguraServicoDeChamados();

var app = builder.Build();

app.UseExceptionHandler("/error");
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
app.UseRouting();
app.ConfiguraChamados(app.Environment);
app.MapControllers();

Log.Logger.Information("#############################################################");
Log.Logger.Information("###              Executando Configurações                 ###");
Log.Logger.Information("#############################################################");

app.Run();