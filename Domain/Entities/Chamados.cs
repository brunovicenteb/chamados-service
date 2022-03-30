namespace Chamados.Service.Domain.Entities;

public class Chamados
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string CPF { get; set; }

    public string Descricao { get; set; }

    public DateTime DataHora { get; set; }
}