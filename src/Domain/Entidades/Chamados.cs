using Chamados.Service.Domain.Enums;
using Chamados.Service.Domain.Interfaces;

namespace Chamados.Service.Domain.Entidades;

/// <summary>Representação de um chamado de cliente.</summary>
public class Chamados : IEntidade<string>
{
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

    /// <summary>Data e Hora da criação do chamado</summary>
    /// <example>10/09/1974 01:17:48</example>
    public DateTime DataHoraCriacao { get; set; }

    /// <summary>Data e Hora da última alteração do chamado</summary>
    /// <example>09/03/1987 10:30:47</example>
    public DateTime? DataHoraUltimaAtualizacao { get; set; }

    public bool Equals(string outro)
    {
        return Id.CompareTo(outro) == 0;
    }
}