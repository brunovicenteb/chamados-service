using System.Text;

namespace Chamados.Service.Toolkit.Domnios;

public readonly struct Nome : IComparable<Nome>, IEquatable<Nome>, IComparable<string>, IEquatable<string>
{
    private const int _TamanhoMinimo = 10;
    private const int _TamanhoMaximo = 50;

    public static bool operator ==(Nome a, Nome b) => a.CompareTo(b) == 0;

    public static bool operator !=(Nome a, Nome b) => !(a == b);

    public static bool operator ==(Nome a, string b) => a.Valor.CompareTo(b) == 0;

    public static bool operator !=(Nome a, string b) => !(a == b);

    public static bool operator ==(string a, Nome b) => a.CompareTo(b.Valor) == 0;

    public static bool operator !=(string a, Nome b) => !(a == b);

    public static implicit operator Nome(string valor) => new Nome(valor);

    public static implicit operator string(Nome nome) => nome.Valor;

    public Nome(string valor = null)
    {
        Valor = valor;
    }

    public string Valor { get; }

    public bool EstaVazio { get => string.IsNullOrEmpty(Valor); }

    public bool EstaPreenchido { get => !EstaVazio; }

    public bool Equals(Nome outro) => this.Valor.Equals(outro.Valor);

    public bool Equals(string outro) => this.Valor.Equals(outro);

    public int CompareTo(Nome outro) => Valor.CompareTo(outro.Valor);

    public int CompareTo(string outro) => Valor.CompareTo(outro);

    public override int GetHashCode() => Valor.GetHashCode();

    public override string ToString() => Valor.ToString();

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        return obj is Nome outro && Equals(outro);
    }

    public string Validar(string nomeDoCampo, bool ehRequerido)
    {
        if (ehRequerido || EstaPreenchido)
        {
            int tam = EstaVazio ? 0 : Valor.Length;
            if (_TamanhoMinimo > tam)
                return $"O Campo {nomeDoCampo} precisa ter mais de {_TamanhoMinimo} caracteres.";
            else if (_TamanhoMaximo < tam)
                return $"O Campo {nomeDoCampo} não pode ter mais de {_TamanhoMaximo} caracteres.";
        }
        return string.Empty;
    }
}