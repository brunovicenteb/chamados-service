namespace Chamados.Service.Domain.Interfaces.Repositorios;

public interface IChamadosRepositorio
{

    Task<long> PegarQuantidadeAsync(bool retornarChamadosFechados);

    Task<IEnumerable<Entities.Chamados>> PegarChamadosAsync(int inicio, int limite);

    Task<Entities.Chamados> PegarChamadoPorIdAsync(string id);

    Task<Entities.Chamados> InserirAsync(Entities.Chamados chamado);

    Task<Entities.Chamados> AtualizarAsync(Entities.Chamados chamado);
}