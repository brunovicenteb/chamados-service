using Chamados.Service.Domain.Enums;
using Chamados.Service.Domain.Interfaces;
using Chamados.Service.Toolkit.Dominios;

namespace Chamados.Service.Domain.Modelos;

/// <summary>Representação de chamado com os dados alterados.</summary>
public class ChamadoAlterado : IEntidade<string>
{
    /// <summary>Identificador do chamado</summary>
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public string Id { get; set; }

    /// <summary>Assunto do chamado</summary>
    /// <example>Trocar a lâmpada do camarim</example>
    public string Assunto { get; set; }

    /// <summary>Gravidade do chamado</summary>
    /// <example>Moderado</example>
    public Gravidade Gravidade { get; set; }

    /// <summary>Nome da pessoa que criou o chamado</summary>
    /// <example>Margot Robbie</example>
    public Nome NomePessoa { get; set; }

    /// <summary>CPF da pessoa que criou o chamado</summary>
    /// <example>31159486549</example>
    public CPF CPF { get; set; }

    /// <summary>E-mail da pessoa que criou o chamado</summary>
    /// <example>margot.robbie@gmail.com</example>
    public Email Email { get; set; }

    /// <summary>Descrição detalhada do chamado</summary>
    /// <example>Lorem ipsum dolor sit amet, consectetur adipiscing elit...</example>
    public string Descricao { get; set; }

    /// <summary>Condição do chamado</summary>
    /// <example>A lâmpada do camarim 07 está queimada. Favor trocar o mais rápido possível! =)</example>
    public bool Aberto { get; set; }

    public bool Equals(string outro)
    {
        return Id.CompareTo(outro) == 0;
    }
}