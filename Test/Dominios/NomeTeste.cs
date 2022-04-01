using Chamados.Service.Toolkit.Domnios;
using NUnit.Framework;

namespace Chamados.Service.Tests.Dominios;

public class NomeTeste
{
    [Test]
    public void TestaCriacao()
    {
        Nome nome = new Nome("Samwise Gamgee");
        Assert.AreEqual("Samwise Gamgee", nome.Valor);

        nome = "Tyler Durden";
        Assert.AreEqual("Tyler Durden", nome.Valor);
    }

    [Test]
    public void TestaComparacaoComOutroNome()
    {
        Nome primeiroNome = new Nome("Lisbeth Salander");
        Nome segundoNome = new Nome("Lisbeth Salander");
        Assert.IsTrue(primeiroNome == segundoNome);
        Assert.IsFalse(primeiroNome != segundoNome);
        Assert.AreEqual(primeiroNome, segundoNome);
        Assert.AreEqual(primeiroNome.Valor, segundoNome.Valor);
    }

    [Test]
    public void TestaComparacaoComString()
    {
        Nome nome = new Nome("Sarah Connor");
        Assert.IsTrue("Sarah Connor" == nome);
        Assert.IsTrue(nome == "Sarah Connor");
        Assert.AreEqual("Sarah Connor", nome);
        Assert.AreEqual(nome, "Sarah Connor");

        Assert.IsTrue("Tony Stark" != nome);
        Assert.IsTrue(nome != "Tony Stark");
        Assert.AreNotEqual("Tony Stark", nome);
        Assert.AreNotEqual(nome, "Tony Stark");
    }

    [Test]
    public void TestaValidacaoRequerido()
    {
        Nome primeiroNome = new Nome("Atleta", true);
        Assert.AreEqual("O Campo Atleta é requerido.", primeiroNome.Validar());

        primeiroNome = "Serena Williams";
        Assert.AreEqual(string.Empty, primeiroNome.Validar());
    }

    [Test]
    public void TestaValidacaoTamanhoMinimo()
    {
        Nome primeiroNome = new Nome("Atleta", true, 20);
        Assert.AreEqual("O Campo Atleta precisa ter mais de 20 caracteres.", primeiroNome.Validar());

        //primeiroNome = "Serena Williams";
        //Assert.AreEqual("O Campo Atleta precisa ter mais de 20 caracteres.", primeiroNome.Validar());

        //primeiroNome = "Maria Esther Andion Bueno";
        //Assert.AreEqual(string.Empty, primeiroNome.Validar());
    }
}