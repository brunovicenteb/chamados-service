using Chamados.Service.Domain.Entidades;
using Microsoft.Extensions.Configuration;

namespace Chamados.Service.Infra.Data.Mongo;

public class RepositorioChamadoPostgres : RepositorioMongo<Chamado, string>
{
    public RepositorioChamadoPostgres(IConfiguration configuration)
        : base(configuration, "Chamados")
    {
    }

    protected override object PegarDadoOrdenacao(Chamado entidade)
    {
        return entidade.DataHoraCriacao;
    }
}