using Chamados.Service.Toolkit.Exceptions;
using Chamados.Service.Domain.Interfaces.Servicos;
using Chamados.Service.Domain.Interfaces.Repositorios;

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
        if (string.IsNullOrEmpty(id))
            throw new BadRequestException("Não é possível encontrar um chamado sem um identificador.");
        Domain.Entities.Chamados c = await _Repositorio.PegarChamadoPorIdAsync(id);
        if (c == null)
            throw new NotFoundException("Não foi possível encontrar o chamado com o identificador informado.");
        return await _Repositorio.PegarChamadoPorIdAsync(id);
    }

    public async Task<IEnumerable<Domain.Entities.Chamados>> PegarChamadosAsync(int? inicio, int? limite)
    {
        int i = inicio ?? 0;
        int l = Math.Min(50, limite ?? 10);
        return await _Repositorio.PegarChamadosAsync(i, l);
    }

    public async Task<long> PegarQuantidadeAsync(bool retornarChamadosFechados)
    {
        return await _Repositorio.PegarQuantidadeAsync(retornarChamadosFechados);
    }

    public async Task<Domain.Entities.Chamados> AtualizarAsync(Domain.Entities.Chamados chamado)
    {
        if (string.IsNullOrEmpty(chamado.Id))
            throw new BadRequestException("Não é possível atualizar um chamado sem um identificador.");
        Domain.Entities.Chamados c = await _Repositorio.PegarChamadoPorIdAsync(chamado.Id);
        if (c == null)
            throw new NotFoundException("Não foi possível atualizar o chamado com o identificador informado.");
        return await _Repositorio.AtualizarAsync(chamado);
    }

    public async Task<Domain.Entities.Chamados> InserirAsync(Domain.Entities.Chamados chamado)
    {
        if (!string.IsNullOrEmpty(chamado.Id))
            throw new BadRequestException("Não é possível inserir um chamado que já possui identificador.");
        return await _Repositorio.InserirAsync(chamado);
    }
}