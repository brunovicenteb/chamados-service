using Chamados.Service.Domain.Interfaces.Repositorios;

namespace Chamados.Service.Application;

public class ServicoChamado : Servico<Domain.Entidades.Chamados, string>
{
    public ServicoChamado(IRepositorio<Domain.Entidades.Chamados, string> repositorio)
        : base(repositorio, "chamado")
    {
    }

    protected override bool EhIdentificadorVazio(string id)
    {
        return string.IsNullOrEmpty(id);
    }
}