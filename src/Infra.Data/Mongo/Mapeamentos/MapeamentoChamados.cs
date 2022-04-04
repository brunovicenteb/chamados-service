using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization.IdGenerators;
using Chamados.Service.Domain.Entidades;

namespace Chamados.Service.Infra.Data.Mongo.Mapeamentos;

public static class MapeamentoChamados
{
    private static long _Mapeado = 0;

    public static void Mapear()
    {
        if (Interlocked.Read(ref _Mapeado) == 1)
            return;
        Interlocked.Exchange(ref _Mapeado, 1);
        BsonClassMap.RegisterClassMap<Chamado>(m =>
        {
            m.AutoMap();
            m.SetIgnoreExtraElements(true);
            m.MapIdMember(c => c.Id)
                .SetIdGenerator(new StringObjectIdGenerator())
                .SetSerializer(new StringSerializer(BsonType.ObjectId))
                .SetIsRequired(true)
                .SetElementName("id");
            m.MapMember(o => o.Gravidade)
                .SetIsRequired(true)
                .SetElementName("gravidade");
            m.MapMember(o => o.NomePessoa)
                .SetIsRequired(true)
                .SetElementName("nomePessoa");
            m.MapMember(o => o.CPF)
                .SetIsRequired(true)
                .SetElementName("cpf");
            m.MapMember(o => o.Email)
                .SetIsRequired(true)
                .SetElementName("email");
            m.MapMember(o => o.Descricao)
                .SetIsRequired(true)
                .SetElementName("descricao");
            m.MapMember(o => o.Aberto)
                .SetIsRequired(true)
                .SetDefaultValue(true)
                .SetElementName("aberto");
            m.MapMember(o => o.DataHoraCriacao)
                .SetIsRequired(true)
                .SetDefaultValue(DateTime.Now)
                .SetElementName("dataHoraCriacao");
            m.MapMember(o => o.DataHoraUltimaAtualizacao)
                .SetIsRequired(false)
                .SetElementName("dataHoraUltimaAtualizacao");
        });
    }
}