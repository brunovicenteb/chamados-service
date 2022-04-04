namespace Chamados.Service.Toolkit.Dominios;

public record StringStruct : IComparable<string>, IEquatable<string>, IComparable<StringStruct>
{
    public static bool operator ==(StringStruct a, string b) => a.Valor.CompareTo(b) == 0;

    public static bool operator !=(StringStruct a, string b) => !(a == b);

    public static bool operator ==(string a, StringStruct b) => a.CompareTo(b.Valor) == 0;

    public static bool operator !=(string a, StringStruct b) => !(a == b);

    public StringStruct(string valor = null)
    {
        Validar(valor);
        Valor = valor;
    }

    public string Valor { get; }

    public bool EstaVazio { get => string.IsNullOrEmpty(Valor); }

    public bool EstaPreenchido { get => !EstaVazio; }

    public int CompareTo(StringStruct outro) => Valor == null && outro.Valor == null ? 0 : Valor.CompareTo(outro.Valor);

    public bool Equals(string outro) => Valor == null && outro == null || Valor.Equals(outro);

    public int CompareTo(string outro) => Valor == null && outro == null ? 0 : Valor.CompareTo(outro);

    public override int GetHashCode() => Valor == null ? 0 : Valor.GetHashCode();

    protected virtual void Validar(string valor)
    {
    }
}