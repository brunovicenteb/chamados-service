using Microsoft.Extensions.Configuration;

namespace Chamados.Service.Infra.Data.Mongo;

public class RepositorioChamadoPostgres : RepositorioMongo<Domain.Entidades.Chamados, string>
{
    public RepositorioChamadoPostgres(IConfiguration configuration)
        : base(configuration, "Chamados")
    {
    }

    protected override object PegarDadoOrdenacao(Domain.Entidades.Chamados entidade)
    {
        return entidade.DataHoraCriacao;
    }
}