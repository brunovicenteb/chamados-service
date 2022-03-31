﻿using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Chamados.Service.Domain.Interfaces.Repositorios;
using Chamados.Service.Toolkit.Exceptions;

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

    public async Task<Domain.Entities.Chamados> PegarChamadoPorIdAsync(string id)
    {
        return await _Chamados.Find(o => o.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Chamados>> PegarChamadosAsync(int pInicio, int pLimite)
    {
        return await _Chamados.Find(FilterDefinition<Domain.Entities.Chamados>.Empty)
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
        var updateResult = await _Chamados.ReplaceOneAsync(
            o => o.Id == chamado.Id, replacement: chamado);
        if (!updateResult.IsAcknowledged || updateResult.ModifiedCount == 0)
            throw new BadRequestException("Não foi possível atualizar o chamado.");
        return await PegarChamadoPorIdAsync(chamado.Id);
    }
}