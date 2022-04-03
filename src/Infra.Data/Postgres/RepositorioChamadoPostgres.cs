using Microsoft.EntityFrameworkCore;

namespace Chamados.Service.Infra.Data.Postgres;

public class RepositorioChamadoPostgres : RepositorioPostgres<Domain.Entidades.Chamados, string>
{
    public RepositorioChamadoPostgres(ContextoPostgres contexto)
        : base(contexto)
    {
    }

    protected override DbSet<Domain.Entidades.Chamados> Colecao => Contexto.Chamados;
}