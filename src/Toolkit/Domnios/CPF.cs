using System.Text;

namespace Chamados.Service.Toolkit.Domnios;

public readonly struct CPF : IComparable<CPF>, IEquatable<CPF>, IComparable<string>, IEquatable<string>
{
    public static bool operator ==(CPF a, CPF b) => a.CompareTo(b) == 0;

    public static bool operator !=(CPF a, CPF b) => !(a == b);

    public static bool operator ==(CPF a, string b) => a.Valor.CompareTo(b) == 0;

    public static bool operator !=(CPF a, string b) => !(a == b);

    public static bool operator ==(string a, CPF b) => a.CompareTo(b.Valor) == 0;

    public static bool operator !=(string a, CPF b) => !(a == b);

    public static implicit operator CPF(string valor) => new CPF(valor);

    public static implicit operator string(CPF nome) => nome.Valor;

    public CPF(string valor = null)
    {
        Valor = valor;
    }

    public string Valor { get; }

    public bool EstaVazio { get => string.IsNullOrEmpty(Valor); }

    public bool EstaPreenchido { get => !EstaVazio; }

    public bool Equals(CPF outro) => this.Valor.Equals(outro.Valor);

    public bool Equals(string outro) => this.Valor.Equals(outro);

    public int CompareTo(CPF outro) => Valor.CompareTo(outro.Valor);

    public int CompareTo(string outro) => Valor.CompareTo(outro);

    public override int GetHashCode() => Valor.GetHashCode();

    public override string ToString() => Valor.ToString();

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        return obj is CPF outro && Equals(outro);
    }

    public string Validar(string nomeDoCampo, bool ehRequerido)
    {
        if (EstaPreenchido)
        {
            if (!EhCpfValido(Valor))
                return $"Campo {nomeDoCampo} possui dados inválidos.";
        }
        if (ehRequerido && EstaVazio)
            return $"O campo {nomeDoCampo} não pode ficar vazio.";
        return string.Empty;
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