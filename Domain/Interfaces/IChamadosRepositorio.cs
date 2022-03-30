namespace Chamados.Service.Domain.Interfaces;

public interface IChamadosRepositorio
{

    IEnumerable<Entities.Chamados> PegaProdutos();
}