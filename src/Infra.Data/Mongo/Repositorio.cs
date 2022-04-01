using MongoDB.Driver;
using Chamados.Service.Toolkit.Excecoes;
using Microsoft.Extensions.Configuration;
using Chamados.Service.Domain.Interfaces;
using Chamados.Service.Domain.Interfaces.Repositorios;

namespace Chamados.Service.Infra.Data.Mongo;

public abstract class Repositorio<T, V> : IRepositorio<T, V> where T : IEntidade<V>
{
    private readonly IMongoCollection<T> _Chamados;

    public Repositorio(IConfiguration pConfiguration, string collectionName)
    {
        var cliente = new MongoClient(pConfiguration.GetValue<string>
            ("DataBaseSettings:ConnectionString"));
        var database = cliente.GetDatabase(pConfiguration.GetValue<string>
            ("DataBaseSettings:DataBaseName"));
        _Chamados = database.GetCollection<T>(collectionName);
    }

    protected abstract object PegarDadoOrdenacao(T entidade);

    public async Task<long> PegarQuantidadeAsync()
    {
        return await _Chamados.CountDocumentsAsync(o => true);
    }

    public async Task<bool> ExcluirAsync(V id)
    {
        var resultadoExclusao = await _Chamados.DeleteOneAsync(o => o.Id.Equals(id));
        return resultadoExclusao.IsAcknowledged && resultadoExclusao.DeletedCount == 1;
    }

    public async Task<T> PegarPorIdAsync(V id)
    {
        return await _Chamados.Find(o => o.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public async Task<IList<T>> PegarAsync(int inicio, int limite)
    {
        return await _Chamados.Find(FilterDefinition<T>.Empty)
            //.SortBy(o => PegarDadoOrdenacao(o))
            .Skip(inicio)
            .Limit(limite).ToListAsync();
    }

    public async Task<T> InserirAsync(T entidade)
    {
        await _Chamados.InsertOneAsync(entidade);
        return await PegarPorIdAsync(entidade.Id);
    }

    public async Task<T> AtualizarAsync(T entidade)
    {
        var resultadoAtualizacao = await _Chamados.ReplaceOneAsync(
            o => o.Id.Equals(entidade.Id), replacement: entidade);
        if (!resultadoAtualizacao.IsAcknowledged || resultadoAtualizacao.ModifiedCount == 0)
            throw new BadRequestException("Não foi possível atualizar o chamado.");
        return await PegarPorIdAsync(entidade.Id);
    }
}