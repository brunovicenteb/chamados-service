using Chamados.Service.Domain.Modelos;
using Chamados.Service.Domain.Entidades;
using Chamados.Service.Domain.Interfaces.Repositorios;

namespace Chamados.Service.Application;

public class ServicoChamado : Servico<Chamado, string, NovoChamado>
{
    public ServicoChamado(IRepositorio<Chamado, string> repositorio)
        : base(repositorio, "chamado")
    {
    }

    protected override void PreencherValoresPadrao(Chamado entidade)
    {
        entidade.Aberto = true;
    }

    protected override bool EhIdentificadorVazio(string id)
    {
        return string.IsNullOrEmpty(id);
    }
}