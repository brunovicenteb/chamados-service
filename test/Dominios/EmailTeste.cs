using Chamados.Service.Toolkit.Domnios;
using NUnit.Framework;

namespace Chamados.Service.Tests.Dominios;

public class EmailTeste
{
    [Test]
    public void TestarCriacao()
    {
        Email Email = new Email("testarcriacao@gmail.com");
        Assert.AreEqual("testarcriacao@gmail.com", Email.Valor);

        Email = "testarcriacao@hotmail.com";
        Assert.AreEqual("testarcriacao@hotmail.com", Email.Valor);
    }

    [Test]
    public void TestarComparacaoComOutroEmail()
    {
        Email primeiroEmail = new Email("testacomparacaocomoutronome@gmail.com");
        Email segundoEmail = new Email("testacomparacaocomoutronome@gmail.com");
        Assert.IsTrue(primeiroEmail == segundoEmail);
        Assert.IsFalse(primeiroEmail != segundoEmail);
        Assert.AreEqual(primeiroEmail, segundoEmail);
        Assert.AreEqual(primeiroEmail.Valor, segundoEmail.Valor);
    }

    [Test]
    public void TestarComparacaoComString()
    {
        Email Email = new Email("testacomparacaocomstring@gmail.com");
        Assert.IsTrue("testacomparacaocomstring@gmail.com" == Email);
        Assert.IsTrue(Email == "testacomparacaocomstring@gmail.com");
        Assert.AreEqual("testacomparacaocomstring@gmail.com", Email);
        Assert.AreEqual(Email, "testacomparacaocomstring@gmail.com");

        Assert.IsTrue("testacomparacaocomstring@hotmail.com" != Email);
        Assert.IsTrue(Email != "testacomparacaocomstring@hotmail.com");
        Assert.AreNotEqual("testacomparacaocomstring@hotmail.com", Email);
        Assert.AreNotEqual(Email, "testacomparacaocomstring@hotmail.com");
    }

    //[Test]
    //public void TestarValidacaoEstaVazio()
    //{
    //    Email Email = string.Empty;
    //    Assert.IsTrue(Email.EstaVazio);

    //    Email = "testavalidacaoestavazio@gmail.com";
    //    Assert.IsFalse(Email.EstaVazio);
    //}

    //[Test]
    //public void TestarValidacaoEstaPreenchido()
    //{
    //    Email Email = string.Empty;
    //    Assert.IsFalse(Email.EstaPreenchido);

    //    Email = "testavalidacaoestapreenchido@gmail.com";
    //    Assert.IsTrue(Email.EstaPreenchido);
    //}

    //[Test]
    //public void TestarValidacaoComCampoNaoRequerido()
    //{
    //    Email Email = string.Empty;
    //    Assert.AreEqual(string.Empty, Email.Validar("Email", false));
    //    Assert.AreEqual(string.Empty, Email.Validar("Email do Cliente", false));
    //}

    //[Test]
    //public void TestarValidacaoComCampoRequerido()
    //{
    //    Email Email = string.Empty;
    //    Assert.AreEqual("O campo Email não pode ficar vazio.", Email.Validar("Email", true));
    //    Assert.AreEqual("O campo Email do Funcionário não pode ficar vazio.", Email.Validar("Email do Funcionário", true));
    //}

    //[Test]
    //public void TestarValidacaoFalhandoComCampoNaoRequerido()
    //{
    //    TestarValidacaoFalhando(false);
    //}

    //[Test]
    //public void TestarValidacaoFalhandoComCampoRequerido()
    //{
    //    TestarValidacaoFalhando(true);
    //}

    //[Test]
    //public void TestarValidacaoPassandoComCampoNaoRequerido()
    //{
    //    TestarValidacaoPassando(false);
    //}

    //[Test]
    //public void TestarValidacaoPassandoComCampoRequerido()
    //{
    //    TestarValidacaoPassando(true);
    //}

    //public void TestarValidacaoFalhando(bool ehRequerido)
    //{
    //    Email Email = "testarvalidacaofalhando";
    //    Assert.AreEqual("Campo Email do Cliente possui dados inválidos.", Email.Validar("Email do Cliente", ehRequerido));

    //    Email = "testarvalidacaofalhando@gmail";
    //    Assert.AreEqual("Campo Email do Funcionário possui dados inválidos.", Email.Validar("Email do Funcionário", ehRequerido));

    //    Email = "validacaofalhandogmail.com";
    //    Assert.AreEqual("Campo Email do Cliente possui dados inválidos.", Email.Validar("Email do Cliente", ehRequerido));
    //}

    //public void TestarValidacaoPassando(bool ehRequerido)
    //{
    //    Email Email = "testavalidacaopassando@gmail.com";
    //    Assert.AreEqual(string.Empty, Email.Validar("Email", ehRequerido));

    //    Email = "testavalidacaopassando@hotmail.com";
    //    Assert.AreEqual(string.Empty, Email.Validar("Email", ehRequerido));
    //}
}