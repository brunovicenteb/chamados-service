using Chamados.Service.Domain.Interfaces.Repositorios;
using Chamados.Service.Domain.Interfaces.Servicos;

namespace Chamados.Service.Application;

public class ChamadosServico : IChamadosServico
{
    private readonly IChamadosRepositorio _Repositorio;

    public ChamadosServico(IChamadosRepositorio repositorio)
    {
        _Repositorio = repositorio;
    }

    public async Task<Domain.Entities.Chamados> PegarChamadoPorIdAsync(string id)
    {
        return await _Repositorio.PegarChamadoPorIdAsync(id);
    }

    public async Task<IEnumerable<Domain.Entities.Chamados>> PegarChamadosAsync(int? inicio, int? limite)
    {
        int i = inicio ?? 0;
        int l = Math.Min(50, limite ?? 10);
        return await _Repositorio.PegarChamadosAsync(i, l);
    }

    public async Task<long> PegarQuantidadeAsync(bool apenasAbertos)
    {
        return await _Repositorio.PegarQuantidadeAsync(apenasAbertos);
    }

    public async Task<Domain.Entities.Chamados> AtualizarAsync(Domain.Entities.Chamados chamado)
    {
        return await _Repositorio.AtualizarAsync(chamado);
    }

    public async Task<Domain.Entities.Chamados> InserirAsync(Domain.Entities.Chamados chamado)
    {
        return await _Repositorio.InserirAsync(chamado);
    }
}