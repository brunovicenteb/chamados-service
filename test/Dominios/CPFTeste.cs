using Chamados.Service.Toolkit.Dominios;
using Chamados.Service.Toolkit.Excecoes;
using NUnit.Framework;

namespace Chamados.Service.Tests.Dominios;

public class CPFTeste
{
    [Test]
    public void TestarCriacao()
    {
        CPF cpf = new CPF("115.930.830-68");
        Assert.AreEqual("115.930.830-68", cpf.Valor);

        cpf = "961.884.040-96";
        Assert.AreEqual("961.884.040-96", cpf.Valor);
    }

    [Test]
    public void TestarComparacaoComOutroCpf()
    {
        CPF primeiroCpf = new CPF("786.996.800-58");
        CPF segundoCpf = new CPF("786.996.800-58");
        Assert.IsTrue(primeiroCpf == segundoCpf);
        Assert.IsFalse(primeiroCpf != segundoCpf);
        Assert.AreEqual(primeiroCpf, segundoCpf);
        Assert.AreEqual(primeiroCpf.Valor, segundoCpf.Valor);
    }

    [Test]
    public void TestarComparacaoComString()
    {
        CPF cpf = new CPF("078.267.030-00");
        Assert.IsTrue("078.267.030-00" == cpf);
        Assert.IsTrue(cpf == "078.267.030-00");
        Assert.AreEqual("078.267.030-00", cpf);
        Assert.AreEqual(cpf, "078.267.030-00");

        Assert.IsTrue("424.288.820-15" != cpf);
        Assert.IsTrue(cpf != "424.288.820-15");
        Assert.AreNotEqual("424.288.820-15", cpf);
        Assert.AreNotEqual(cpf, "424.288.820-15");
    }

    [Test]
    public void TestarValidacaoComCPFVazio()
    {
        Assert.Catch<BadRequestException>(() => SetCPF(""), $"O CPF \"\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF(null), $"O CPF \"\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF(string.Empty), $"O CPF \"\" não é válido.");
    }

    [Test]
    public void TestarValidacaoComCPFPreenchido()
    {
        Assert.Catch<BadRequestException>(() => SetCPF("1"), $"O CPF \"1\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF("12"), $"O CPF \"12\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF("123"), $"O CPF \"123\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF("1234"), $"O CPF \"1234\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF("12345"), $"O CPF \"12345\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF("123456"), $"O CPF \"123456\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF("1234567"), $"O CPF \"1234567\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF("12345678"), $"O CPF \"12345678\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF("123456789"), $"O CPF \"123456789\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF("1234567891"), $"O CPF \"1234567891\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF("12345678910"), $"O CPF \"12345678910\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF("123456789101"), $"O CPF \"123456789101\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF("07277907098"), $"O CPF \"07277907098\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF("26270311087"), $"O CPF \"26270311087\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetCPF("34612552057"), $"O CPF \"34612552057\" não é válido.");
    }

    private void SetCPF(string valor)
    {
        CPF email = new CPF(valor);
    }
}