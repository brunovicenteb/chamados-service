using MongoDB.Bson;
using Chamados.Service.Domain.Enums;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Chamados.Service.Domain.Entities;

/// <summary>Representação de um chamado de cliente.</summary>
public class Chamados
{
    [BsonId]
    [JsonPropertyName("id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id
    {
        get; set;
    }

    /// <summary>Assunto do artigo</summary>
    /// <example>Problema na consulta de clientes</example>
    [JsonPropertyName("assunto")]
    [BsonRepresentation(BsonType.String)]
    public string Assunto { get; set; }

    /// <summary>Gravidade do chamado</summary>
    /// <example>Moderado</example>
    [JsonPropertyName("gravidade")]
    [BsonRepresentation(BsonType.String)]
    public Gravidade Gravidade { get; set; }

    /// <summary>Nome da pessoa que criou o chamado</summary>
    /// <example>Margot Robbie</example>
    [JsonPropertyName("nomePessoa")]
    [BsonRepresentation(BsonType.String)]
    public string NomePessoa { get; set; }

    /// <summary>CPF da pessoa que criou o chamado</summary>
    /// <example>31159486549</example>
    [JsonPropertyName("cpf")]
    [BsonRepresentation(BsonType.String)]
    public string CPF { get; set; }

    /// <summary>E-mail da pessoa que criou o chamado</summary>
    /// <example>margot.robbie@gmail.com</example>
    [JsonPropertyName("email")]
    [BsonRepresentation(BsonType.String)]
    public string Email { get; set; }


    /// <summary>Descrição detalhada do chamado</summary>
    /// <example>Lorem ipsum dolor sit amet, consectetur adipiscing elit...</example>
    [JsonPropertyName("descricao")]
    [BsonRepresentation(BsonType.String)]
    public string Descricao { get; set; }

    /// <summary>Condição do chamado/summary>
    /// <example>true</example>
    [JsonPropertyName("aberto")]
    [BsonRepresentation(BsonType.Boolean)]
    public bool Aberto { get; set; }

    /// <summary>Data e Hora da criação do chamadosummary>
    /// <example>10/09/1974 01:17:48</example>
    [JsonPropertyName("dataHoraCriacao")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime DataHoraCriacao { get; set; }

    /// <summary>Data e Hora da última alteração do chamado<summary>
    /// <example>09/03/1987 10:30:47</example>
    [JsonPropertyName("dataHoraUltimaAtualizacao")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime? DataHoraUltimaAtualizacao { get; set; }
}