using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Chamados.Service.Toolkit.Excecoes;
using Chamados.Service.Domain.Interfaces.Repositorios;

namespace Chamados.Service.Infra.Data;

public class ChamadosRepositorio : IChamadosRepositorio
{
    private readonly IMongoCollection<Domain.Entities.Chamados> _Chamados;

    public ChamadosRepositorio(IConfiguration pConfiguration)
    {
        var cliente = new MongoClient(pConfiguration.GetValue<string>
            ("DataBaseSettings:ConnectionString"));
        var database = cliente.GetDatabase(pConfiguration.GetValue<string>
            ("DataBaseSettings:DataBaseName"));
        _Chamados = database.GetCollection<Domain.Entities.Chamados>("Chamados");
    }

    public async Task<long> PegarQuantidadeAsync(bool retornarChamadosFechados)
    {
        if (retornarChamadosFechados)
            return await _Chamados.CountDocumentsAsync(o => true);
        return await _Chamados.CountDocumentsAsync(o => o.Aberto);
    }

    public async Task<bool> ExcluirAsync(string id)
    {
        var resultadoExclusao = await _Chamados.DeleteOneAsync(o => o.Id == id);
        return resultadoExclusao.IsAcknowledged && resultadoExclusao.DeletedCount == 1;
    }

    public async Task<Domain.Entities.Chamados> PegarChamadoPorIdAsync(string id)
    {
        return await _Chamados.Find(o => o.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IList<Domain.Entities.Chamados>> PegarChamadosAsync(int pInicio, int pLimite)
    {
        return await _Chamados.Find(FilterDefinition<Domain.Entities.Chamados>.Empty)
            .SortBy(o => o.DataHoraCriacao)
            .Skip(pInicio)
            .Limit(pLimite).ToListAsync();
    }

    public async Task<Domain.Entities.Chamados> InserirAsync(Domain.Entities.Chamados chamado)
    {
        await _Chamados.InsertOneAsync(chamado);
        return await PegarChamadoPorIdAsync(chamado.Id);
    }

    public async Task<Domain.Entities.Chamados> AtualizarAsync(Domain.Entities.Chamados chamado)
    {
        var resultadoAtualizacao = await _Chamados.ReplaceOneAsync(
            o => o.Id == chamado.Id, replacement: chamado);
        if (!resultadoAtualizacao.IsAcknowledged || resultadoAtualizacao.ModifiedCount == 0)
            throw new BadRequestException("Não foi possível atualizar o chamado.");
        return await PegarChamadoPorIdAsync(chamado.Id);
    }
}