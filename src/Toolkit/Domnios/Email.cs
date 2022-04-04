using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Chamados.Service.Toolkit.Excecoes;

namespace Chamados.Service.Toolkit.Domnios;

public record Email : StringStruct
{
    #region ConversorJson

    public class EmailJsonConverter : JsonConverter<Email>
    {
        public override Email Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Email(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Email value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    //private class EmailJsonConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        return objectType == typeof(Email);
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        var id = (Email)value;
    //        serializer.Serialize(writer, id.Valor);
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        var valor = serializer.Deserialize<string>(reader);
    //        return new Email(valor);
    //    }
    //}

    //public class EmailTypeConverter : TypeConverter
    //{
    //    public override bool CanConvertFrom(ITypeDescriptorContext contexto, Type tipoOriginal)
    //    {
    //        return tipoOriginal == typeof(string) || base.CanConvertFrom(contexto, tipoOriginal);
    //    }

    //    public override object ConvertFrom(ITypeDescriptorContext contexto, CultureInfo cultura, object valor)
    //    {
    //        var stringValue = valor as string;
    //        if (!string.IsNullOrEmpty(stringValue))
    //            return new Email(stringValue);
    //        return base.ConvertFrom(contexto, cultura, valor);
    //    }
    //}
    #endregion

    public static implicit operator Email(string valor) => new Email(valor);

    public static implicit operator string(Email nome) => nome.Valor;

    public Email(string valor = null)
        : base(valor)
    {
    }

    public override string ToString() => Valor?.ToString();

    protected override void Validar(string valor)
    {
        base.Validar(valor);
        if (!EhEmailValido(valor))
            throw new BadRequestException($"O e-mail {valor} não é válido.");
    }

    private bool EhEmailValido(string valor)
    {
        return Regex.IsMatch(valor, "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])");
    }
}