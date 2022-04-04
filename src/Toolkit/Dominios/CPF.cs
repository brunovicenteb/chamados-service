using System.Text.Json;
using System.Text.Json.Serialization;
using Chamados.Service.Toolkit.Excecoes;

namespace Chamados.Service.Toolkit.Dominios;

public record CPF : StringStruct
{
    #region ConversorJson

    public class CPFJsonConverter : JsonConverter<CPF>
    {
        public override CPF Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new CPF(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, CPF value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
    #endregion

    public static implicit operator CPF(string valor) => new CPF(valor);

    public static implicit operator string(CPF nome) => nome.Valor;

    public CPF(string valor = null)
        : base(valor)
    {
    }

    public override string ToString() => Valor?.ToString();

    protected override void Validar(string valor)
    {
        base.Validar(valor);
        if (string.IsNullOrEmpty(valor) || !EhCpfValido(valor))
            throw new BadRequestException($"O CPF \"{valor}\" não é válido.");
    }

    private bool EhCpfValido(string cpf)
    {
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;
        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;
        tempCpf = cpf.Substring(0, 9);
        soma = 0;
        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto.ToString();
        return cpf.EndsWith(digito);
    }
}