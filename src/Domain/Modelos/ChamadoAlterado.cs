using Chamados.Service.Domain.Enums;
using Chamados.Service.Domain.Interfaces;

namespace Chamados.Service.Domain.Modelos;

/// <summary>Representação de chamado com os dados alterados.</summary>
public class ChamadoAlterado : IEntidade<string>
{
    /// <summary>Identificador do chamado</summary>
    public string Id { get; set; }

    /// <summary>Assunto do artigo</summary>
    /// <example>Problema na consulta de clientes</example>
    public string Assunto { get; set; }

    /// <summary>Gravidade do chamado</summary>
    /// <example>Moderado</example>
    public Gravidade Gravidade { get; set; }

    /// <summary>Nome da pessoa que criou o chamado</summary>
    /// <example>Margot Robbie</example>
    public string NomePessoa { get; set; }

    /// <summary>CPF da pessoa que criou o chamado</summary>
    /// <example>31159486549</example>
    public string CPF { get; set; }

    /// <summary>E-mail da pessoa que criou o chamado</summary>
    /// <example>margot.robbie@gmail.com</example>
    public string Email { get; set; }

    /// <summary>Descrição detalhada do chamado</summary>
    /// <example>Lorem ipsum dolor sit amet, consectetur adipiscing elit...</example>
    public string Descricao { get; set; }

    /// <summary>Condição do chamado</summary>
    /// <example>true</example>
    public bool Aberto { get; set; }

    public bool Equals(string outro)
    {
        return Id.CompareTo(outro) == 0;
    }
}