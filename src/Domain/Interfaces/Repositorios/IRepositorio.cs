namespace Chamados.Service.Domain.Interfaces.Repositorios;

public interface IRepositorio<T, V> where T : IEntidade<V>
{

    Task<long> PegarQuantidadeAsync();

    Task<IList<T>> PegarAsync(int inicio, int limite);

    Task<T> PegarPorIdAsync(V id);

    Task<T> InserirAsync(T entidade);

    Task<T> AtualizarAsync(T entidade);

    Task<bool> ExcluirAsync(V id);
}