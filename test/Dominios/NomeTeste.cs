using Chamados.Service.Toolkit.Domnios;
using NUnit.Framework;

namespace Chamados.Service.Tests.Dominios;

public class NomeTeste
{
    [Test]
    public void TestarCriacao()
    {
        Nome nome = new Nome("Samwise Gamgee");
        Assert.AreEqual("Samwise Gamgee", nome.Valor);

        nome = "Tyler Durden";
        Assert.AreEqual("Tyler Durden", nome.Valor);
    }

    [Test]
    public void TestarComparacaoComOutroNome()
    {
        Nome primeiroNome = new Nome("Lisbeth Salander");
        Nome segundoNome = new Nome("Lisbeth Salander");
        Assert.IsTrue(primeiroNome == segundoNome);
        Assert.IsFalse(primeiroNome != segundoNome);
        Assert.AreEqual(primeiroNome, segundoNome);
        Assert.AreEqual(primeiroNome.Valor, segundoNome.Valor);
    }

    [Test]
    public void TestarComparacaoComString()
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
    public void TestarValidacaoEstaVazio()
    {
        Nome primeiroNome = string.Empty;
        Assert.IsTrue(primeiroNome.EstaVazio);

        primeiroNome = "Capitão Kirk";
        Assert.IsFalse(primeiroNome.EstaVazio);
    }

    [Test]
    public void TestarValidacaoEstaPreenchido()
    {
        Nome primeiroNome = string.Empty;
        Assert.IsFalse(primeiroNome.EstaPreenchido);

        primeiroNome = "Smeagol";
        Assert.IsTrue(primeiroNome.EstaPreenchido);
    }

    //[Test]
    //public void TestarValidacaoComCampoNaoRequerido()
    //{
    //    Nome primeiroNome = string.Empty;
    //    Assert.AreEqual(string.Empty, primeiroNome.Validar("Atriz", false));
    //}

    //[Test]
    //public void TestarValidacaoDeTamanhoMinimoComCampoNaoRequerido()
    //{
    //    TestarTamanhoMinimo(false);
    //}

    //[Test]
    //public void TestarValidacaoDeTamanhoMinimoComCampoRequerido()
    //{
    //    TestarTamanhoMinimo(true);
    //}

    //[Test]
    //public void TestarValidacaoDeTamanhoMaximoComCampoNaoRequerido()
    //{
    //    TestarTamanhoMaximo(false);
    //}

    //[Test]
    //public void TestarValidacaoDeTamanhoMaximoComCampoRequerido()
    //{
    //    TestarTamanhoMaximo(true);
    //}

    //private void TestarTamanhoMinimo(bool ehRequerido)
    //{
    //    Nome nome = "Maria";
    //    Assert.AreEqual("O Campo Tenista precisa ter mais de 10 caracteres.", nome.Validar("Tenista", ehRequerido));

    //    nome = "Maria Esther Andion Bueno";
    //    Assert.AreEqual(string.Empty, nome.Validar("Tenista", ehRequerido));
    //}

    //private void TestarTamanhoMaximo(bool ehRequerido)
    //{
    //    Nome nome = "Pedro de Alcântara Francisco Antônio João Carlos Xavier de Paula Miguel...";
    //    Assert.AreEqual("O Campo Imperador não pode ter mais de 50 caracteres.", nome.Validar("Imperador", ehRequerido));

    //    nome = "Dom Pedro I";
    //    Assert.AreEqual(string.Empty, nome.Validar("Imperador", ehRequerido));
    //}
}