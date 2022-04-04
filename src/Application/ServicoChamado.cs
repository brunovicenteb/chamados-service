using Chamados.Service.Domain.Modelos;
using Chamados.Service.Domain.Entidades;
using Chamados.Service.Domain.Interfaces.Repositorios;

namespace Chamados.Service.Application;

public class ServicoChamado : Servico<Chamado, string, NovoChamado, ChamadoAlterado>
{
    public ServicoChamado(IRepositorio<Chamado, string> repositorio)
        : base(repositorio, "chamado")
    {
    }

    protected override bool EhIdentificadorVazio(string id)
    {
        return string.IsNullOrEmpty(id);
    }

    protected override void EntidadeInserida(Chamado entidade)
    {
        entidade.Aberto = true;
    }

    protected override void EntidadeAtualizada(Chamado entidade)
    {
        entidade.DataHoraUltimaAtualizacao = DateTime.Now;
    }
}