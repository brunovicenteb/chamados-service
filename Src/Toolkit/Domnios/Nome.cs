using System.Text;

namespace Chamados.Service.Toolkit.Domnios;

public readonly struct Nome : IComparable<Nome>, IEquatable<Nome>, IComparable<string>, IEquatable<string>
{
    public static bool operator ==(Nome a, Nome b) => a.CompareTo(b) == 0;

    public static bool operator !=(Nome a, Nome b) => !(a == b);

    public static bool operator ==(Nome a, string b) => a.Valor.CompareTo(b) == 0;

    public static bool operator !=(Nome a, string b) => !(a == b);

    public static bool operator ==(string a, Nome b) => a.CompareTo(b.Valor) == 0;

    public static bool operator !=(string a, Nome b) => !(a == b);

    public static implicit operator Nome(string valor) => new Nome(valor);

    public static implicit operator string(Nome nome) => nome.Valor;

    public Nome(string valor)
        : this(valor, string.Empty, false)
    {
    }

    public Nome(string nomeCampo, bool ehRequerido, int? tamanhoMinimo = null, int? tamanhoMaximo = null)
        : this(null, nomeCampo, ehRequerido, tamanhoMinimo, tamanhoMaximo)
    {
    }

    public Nome(string valor, string nomeCampo, bool ehRequerido, int? tamanhoMinimo = null, int? tamanhoMaximo = null)
    {
        Valor = valor;
        _NomeCampo = nomeCampo;
        _EhRequerido = ehRequerido;
        _TamanhoMinimo = tamanhoMinimo;
        _TamanhoMaximo = tamanhoMaximo;
    }

    private readonly string _NomeCampo;
    private readonly bool _EhRequerido;
    private readonly int? _TamanhoMinimo;
    private readonly int? _TamanhoMaximo;

    public string Valor { get; }

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

    public string Validar()
    {
        int tam = Valor == null ? 0 : Valor.Length;
        if (_TamanhoMinimo.HasValue && _TamanhoMinimo.Value > tam)
            return $"O Campo {_NomeCampo} precisa ter mais de {_TamanhoMinimo.Value} caracteres.";
        else if (_TamanhoMaximo.HasValue && _TamanhoMaximo.Value < tam)
            return $"O Campo {_NomeCampo} não pode ter mais de {_TamanhoMaximo.Value} caracteres.";
        else if (_EhRequerido && string.IsNullOrEmpty(Valor))
            return $"O Campo {_NomeCampo} é requerido.";
        return string.Empty;
    }
}