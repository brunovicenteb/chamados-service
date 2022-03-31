namespace Chamados.Service.Domain.Interfaces.Servicos;

public interface IChamadosServico
{
    Task<long> PegarQuantidadeAsync(bool retornarChamadosFechados);

    Task<IEnumerable<Entities.Chamados>> PegarChamadosAsync(int? inicio, int? limite);

    Task<Entities.Chamados> PegarChamadoPorIdAsync(string id);

    Task<Entities.Chamados> InserirAsync(Entities.Chamados chamado);

    Task<Entities.Chamados> AtualizarAsync(Entities.Chamados chamado);

    Task<bool> ExlcuirAsync(string id);
}