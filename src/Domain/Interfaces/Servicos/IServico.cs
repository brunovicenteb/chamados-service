namespace Chamados.Service.Domain.Interfaces.Servicos;

public interface IServico<T, V, I> where T : IEntidade<V>
{
    Task<long> PegarQuantidadeAsync();

    Task<IList<T>> PegarAsync(int? inicio, int? limite);

    Task<T> PegarPorIdAsync(V id);

    Task<T> InserirAsync(I entidade);

    Task<T> AtualizarAsync(T entidade);

    Task<bool> ExcluirAsync(V id);
}