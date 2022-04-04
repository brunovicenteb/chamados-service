using System.Text.Json;
using System.Text.Json.Serialization;

namespace Chamados.Service.Toolkit.Domnios;

public record Nome : StringStruct
{
    #region ConversorJson
    public class NomeJsonConverter : JsonConverter<Nome>
    {
        public override Nome Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Nome(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Nome value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
    #endregion

    public static implicit operator Nome(string valor) => new Nome(valor);

    public static implicit operator string(Nome nome) => nome.Valor;

    public Nome(string valor = null)
        : base(valor)
    {
    }

    public override string ToString() => Valor?.ToString();
}