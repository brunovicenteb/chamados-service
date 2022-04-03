using MongoDB.Driver;
using Chamados.Service.Toolkit.Excecoes;
using Microsoft.Extensions.Configuration;
using Chamados.Service.Domain.Interfaces;
using Chamados.Service.Domain.Interfaces.Repositorios;

namespace Chamados.Service.Infra.Data.Mongo;

public abstract class RepositorioMongo<T, V> : IRepositorio<T, V> where T : IEntidade<V> where V : class
{
    public RepositorioMongo(IConfiguration configuracoes, string nomeDaColecao)
    {
        var cliente = new MongoClient(configuracoes.GetValue<string>
            ("MongoDBSettings:ConnectionString"));
        var database = cliente.GetDatabase(configuracoes.GetValue<string>
            ("MongoDBSettings:DataBaseName"));
        _Colecao = database.GetCollection<T>(nomeDaColecao);
    }

    private readonly IMongoCollection<T> _Colecao;

    protected abstract object PegarDadoOrdenacao(T entidade);

    public async Task<long> PegarQuantidadeAsync()
    {
        return await _Colecao.CountDocumentsAsync(o => true);
    }

    public async Task<bool> ExcluirAsync(V id)
    {
        var resultadoExclusao = await _Colecao.DeleteOneAsync(o => o.Id.Equals(id));
        return resultadoExclusao.IsAcknowledged && resultadoExclusao.DeletedCount == 1;
    }

    public async Task<T> PegarPorIdAsync(V id)
    {
        return await _Colecao.Find(o => o.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public async Task<IList<T>> PegarAsync(int inicio, int limite)
    {
        return await _Colecao.Find(FilterDefinition<T>.Empty)
            //.SortBy(o => PegarDadoOrdenacao(o))
            .Skip(inicio)
            .Limit(limite).ToListAsync();
    }

    public async Task<T> InserirAsync(T entidade)
    {
        await _Colecao.InsertOneAsync(entidade);
        return await PegarPorIdAsync(entidade.Id);
    }

    public async Task<T> AtualizarAsync(T entidade)
    {
        var resultadoAtualizacao = await _Colecao.ReplaceOneAsync(
            o => o.Id.Equals(entidade.Id), replacement: entidade);
        if (!resultadoAtualizacao.IsAcknowledged || resultadoAtualizacao.ModifiedCount == 0)
            throw new BadRequestException("Não foi possível atualizar o chamado.");
        return await PegarPorIdAsync(entidade.Id);
    }
}