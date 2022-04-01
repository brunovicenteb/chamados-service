using Chamados.Service.Toolkit.Domnios;
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
    public void TestarValidacaoEstaVazio()
    {
        CPF cpf = string.Empty;
        Assert.IsTrue(cpf.EstaVazio);

        cpf = "996.750.520-66";
        Assert.IsFalse(cpf.EstaVazio);
    }

    [Test]
    public void TestarValidacaoEstaPreenchido()
    {
        CPF cpf = string.Empty;
        Assert.IsFalse(cpf.EstaPreenchido);

        cpf = "995.567.680-92";
        Assert.IsTrue(cpf.EstaPreenchido);
    }

    [Test]
    public void TestarValidacaoComCampoNaoRequerido()
    {
        CPF cpf = string.Empty;
        Assert.AreEqual(string.Empty, cpf.Validar("CPF", false));
        Assert.AreEqual(string.Empty, cpf.Validar("CPF do Cliente", false));
    }

    [Test]
    public void TestarValidacaoComCampoRequerido()
    {
        CPF cpf = string.Empty;
        Assert.AreEqual("O campo CPF não pode ficar vazio.", cpf.Validar("CPF", true));
        Assert.AreEqual("O campo CPF do Funcionário não pode ficar vazio.", cpf.Validar("CPF do Funcionário", true));
    }

    [Test]
    public void TestarValidacaoFalhandoComCampoNaoRequerido()
    {
        TestarValidacaoFalhando(false);
    }

    [Test]
    public void TestarValidacaoFalhandoComCampoRequerido()
    {
        TestarValidacaoFalhando(true);
    }

    [Test]
    public void TestarValidacaoPassandoComCampoNaoRequerido()
    {
        TestarValidacaoPassando(false);
    }

    [Test]
    public void TestarValidacaoPassandoComCampoRequerido()
    {
        TestarValidacaoPassando(true);
    }

    public void TestarValidacaoFalhando(bool ehRequerido)
    {
        CPF cpf = "563.199.270-32";
        Assert.AreEqual("Campo CPF do Cliente possui dados inválidos.", cpf.Validar("CPF do Cliente", ehRequerido));

        cpf = "563.199.270";
        Assert.AreEqual("Campo CPF do Funcionário possui dados inválidos.", cpf.Validar("CPF do Funcionário", ehRequerido));

        cpf = "939.165.280-843";
        Assert.AreEqual("Campo CPF do Cliente possui dados inválidos.", cpf.Validar("CPF do Cliente", ehRequerido));
    }

    public void TestarValidacaoPassando(bool ehRequerido)
    {
        CPF cpf = "193.034.770-73";
        Assert.AreEqual(string.Empty, cpf.Validar("CPF", ehRequerido));

        cpf = "679.288.820-60";
        Assert.AreEqual(string.Empty, cpf.Validar("CPF", ehRequerido));
    }
}