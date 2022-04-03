using Microsoft.EntityFrameworkCore;
using Chamados.Service.Domain.Interfaces;
using Chamados.Service.Domain.Interfaces.Repositorios;

namespace Chamados.Service.Infra.Data.Postgres;

public abstract class RepositorioPostgres<T, V> : IRepositorio<T, V> where T : class, IEntidade<V>
{
    public RepositorioPostgres(ContextoPostgres contexto)
    {
        Contexto = contexto;
    }

    protected readonly ContextoPostgres Contexto;

    protected abstract DbSet<T> Colecao { get; }

    public async Task<long> PegarQuantidadeAsync()
    {
        return await Colecao.LongCountAsync();
    }

    public async Task<bool> ExcluirAsync(V id)
    {
        T entidade = await PegarPorIdAsync(id);
        Colecao.Remove(entidade);
        return await Contexto.SaveChangesAsync() == 1;
    }

    public async Task<T> PegarPorIdAsync(V id)
    {
        return await Colecao
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id.Equals(id));
    }

    public async Task<IList<T>> PegarAsync(int inicio, int limite)
    {
        return await Colecao
            .Skip(inicio)
            .Take(limite)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T> InserirAsync(T entidade)
    {
        Colecao.Add(entidade);
        await Contexto.SaveChangesAsync();
        return await PegarPorIdAsync(entidade.Id);
    }

    public async Task<T> AtualizarAsync(T entidade)
    {
        Colecao.Update(entidade);
        await Contexto.SaveChangesAsync();
        return await PegarPorIdAsync(entidade.Id);

    }
}