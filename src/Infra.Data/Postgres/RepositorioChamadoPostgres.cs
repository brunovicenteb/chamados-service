using Microsoft.EntityFrameworkCore;
using Chamados.Service.Domain.Entidades;

namespace Chamados.Service.Infra.Data.Postgres;

public class RepositorioChamadoPostgres : RepositorioPostgres<Chamado, string>
{
    public RepositorioChamadoPostgres(ContextoPostgres contexto)
        : base(contexto)
    {
    }

    protected override DbSet<Chamado> Colecao => Contexto.Chamados;
}