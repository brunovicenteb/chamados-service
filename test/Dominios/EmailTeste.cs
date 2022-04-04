using Chamados.Service.Toolkit.Dominios;
using Chamados.Service.Toolkit.Excecoes;
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

    [Test]
    public void TestarValidacaoComEmailVazio()
    {
        Assert.Catch<BadRequestException>(() => SetEmail(""), $"O e-mail \"\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetEmail(null), $"O e-mail \"\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetEmail(string.Empty), $"O e-mail \"\" não é válido.");
    }

    [Test]
    public void TestarValidacaoEstaPreenchido()
    {
        Assert.Catch<BadRequestException>(() => SetEmail("teste@"), $"O e-mail \"teste\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetEmail("teste@"), $"O e-mail \"teste@\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetEmail("teste@gmail"), $"O e-mail \"teste@gmail\" não é válido.");
        Assert.Catch<BadRequestException>(() => SetEmail("teste@gmail."), $"O e-mail \"teste@gmail.\" não é válido.");
    }

    private void SetEmail(string valor)
    {
        Email email = new Email(valor);
    }
}