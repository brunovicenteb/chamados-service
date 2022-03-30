namespace Chamados.Service.Domain.Interfaces.Repositorios;

public interface IChamadosRepositorio
{

    Task<long> PegarQuantidadeAsync(bool apenasAbertos);

    Task<IEnumerable<Entities.Chamados>> PegarChamadosAsync(int pInicio, int pLimite);

    Task<Entities.Chamados> PegarChamadoPorIdAsync(int id);

    Task<bool> InserirAsync(Entities.Chamados chamado);

    Task<bool> AtualizarAsync(Entities.Chamados chamado);
}