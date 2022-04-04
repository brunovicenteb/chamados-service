using Chamados.Service.Domain.Enums;
using Chamados.Service.Toolkit.Dominios;

namespace Chamados.Service.Domain.Modelos;

/// <summary>Representação de um novo chamado cliente.</summary>
public class NovoChamado
{
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
    /// <example>A lâmpada do camarim 07 está queimada. Favor trocar o mais rápido possível! =)</example>
    public string Descricao { get; set; }
}