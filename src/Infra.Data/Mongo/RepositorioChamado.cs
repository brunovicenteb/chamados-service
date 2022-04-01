using Microsoft.Extensions.Configuration;

namespace Chamados.Service.Infra.Data.Mongo;

public class RepositorioChamado : Repositorio<Domain.Entidades.Chamados, string>
{
    public RepositorioChamado(IConfiguration configuration)
        : base(configuration, "Chamados")
    {
    }

    protected override object PegarDadoOrdenacao(Domain.Entidades.Chamados entidade)
    {
        return entidade.DataHoraCriacao;
    }
}