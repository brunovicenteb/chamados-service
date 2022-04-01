using System.Text;
using System.Text.RegularExpressions;

namespace Chamados.Service.Toolkit.Domnios;

public readonly struct Email : IComparable<Email>, IEquatable<Email>, IComparable<string>, IEquatable<string>
{
    public static bool operator ==(Email a, Email b) => a.CompareTo(b) == 0;

    public static bool operator !=(Email a, Email b) => !(a == b);

    public static bool operator ==(Email a, string b) => a.Valor.CompareTo(b) == 0;

    public static bool operator !=(Email a, string b) => !(a == b);

    public static bool operator ==(string a, Email b) => a.CompareTo(b.Valor) == 0;

    public static bool operator !=(string a, Email b) => !(a == b);

    public static implicit operator Email(string valor) => new Email(valor);

    public static implicit operator string(Email Email) => Email.Valor;

    public Email(string valor = null)
    {
        Valor = valor;
    }

    public string Valor { get; }

    public bool EstaVazio { get => string.IsNullOrEmpty(Valor); }

    public bool EstaPreenchido { get => !EstaVazio; }

    public bool Equals(Email outro) => this.Valor.Equals(outro.Valor);

    public bool Equals(string outro) => this.Valor.Equals(outro);

    public int CompareTo(Email outro) => Valor.CompareTo(outro.Valor);

    public int CompareTo(string outro) => Valor.CompareTo(outro);

    public override int GetHashCode() => Valor.GetHashCode();

    public override string ToString() => Valor.ToString();

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        return obj is Email outro && Equals(outro);
    }

    public string Validar(string nomeDoCampo, bool ehRequerido)
    {
        if (EstaPreenchido)
        {
            if (!EhEmailValido(Valor))
                return $"Campo {nomeDoCampo} possui dados inválidos.";
        }
        if (ehRequerido && EstaVazio)
            return $"O campo {nomeDoCampo} não pode ficar vazio.";
        return string.Empty;
    }

    private bool EhEmailValido(string valor)
    {
        return Regex.IsMatch(valor, "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])");
    }
}