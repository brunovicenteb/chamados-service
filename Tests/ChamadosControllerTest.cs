using System;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Chamados.Service.Tests.Mock;
using Chamados.Service.Application;
using Chamados.Service.Domain.Enums;
using Chamados.Service.Api.Controllers;
using Chamados.Service.Domain.Interfaces.Servicos;
using Chamados.Service.Domain.Interfaces.Repositorios;

namespace Chamados.Service.Tests;
public class ArticleControllerTest
{
    private const string _JamesCameronId = "6244c7ea757c678abca6716d";

    [Test]
    public void TestaPegarQuantidade()
    {
        ChamadoController c = CriarController(true);
        IActionResult result = c.PegarQuantidade(true).Result;
        long longResult = AfirmarOk<long>(result, 20);
        Assert.AreEqual(longResult, 20);
    }

    [Test]
    public void TestaPegarQuantidadeSemChamadosFechados()
    {
        ChamadoController c = CriarController(true);
        IActionResult result = c.PegarQuantidade(false).Result;
        long longResult = AfirmarOk<long>(result, 16);
        Assert.AreEqual(16, longResult);
    }

    [Test]
    public void TestaPegarChamadoPorId()
    {
        ChamadoController c = CriarController(true);
        IActionResult resultado = c.PegarChamadoPorId(_JamesCameronId).Result;
        var chamado = AfirmarOk<Domain.Entities.Chamados>(resultado, c);
        Assert.IsFalse(string.IsNullOrEmpty(chamado.Id));
        Assert.IsFalse(chamado.Aberto);
        Assert.AreEqual("Limpesa das lentes", chamado.Assunto);
        Assert.AreEqual("James Cameron", chamado.NomePessoa);
        Assert.AreEqual(Gravidade.Moderado, chamado.Gravidade);
        Assert.AreEqual("82926196075", chamado.CPF);
        Assert.AreEqual("james.cameron@gmail.com", chamado.Email);
        Assert.AreEqual("Precisamos limpar tudo semanalmente.", chamado.Descricao);
    }

    [Test]
    public void TestaPegarChamadoPorIdComIdInexistente()
    {
        ChamadoController c = CriarController(true);
        IActionResult resultado = c.PegarChamadoPorId(Guid.NewGuid().ToString()).Result;
        AfirmarNotFound(resultado, "Não foi possível encontrar o chamado com o identificador informado.");
    }

    [Test]
    public void TestaPegarChamadoPorIdSemPassarId()
    {
        ChamadoController c = CriarController(true);
        IActionResult resultado = c.PegarChamadoPorId(string.Empty).Result;
        AfirmarBadRequest(resultado, "Não é possível encontrar um chamado sem um identificador.");
    }

    [Test]
    public void TestaCriarChamado()
    {
        var chamado = CriarChamado(string.Empty, "Maria Sharapova", "Substituição de Raquete", Gravidade.Bloqueador, "39815683039",
            "maria.sharapova@gmail.com", "Preciso repor minha raquete de treinos com urgência.");
        ChamadoController c = CriarController(false);
        IActionResult resultado = c.Inserir(chamado).Result;
        var resultadoChamado = AfirmarOkCriado<Domain.Entities.Chamados>(resultado, c);
        Assert.IsFalse(string.IsNullOrEmpty(resultadoChamado.Id));
        Assert.IsTrue(resultadoChamado.Aberto);
        Assert.AreEqual("Maria Sharapova", resultadoChamado.NomePessoa);
        Assert.AreEqual("Substituição de Raquete", resultadoChamado.Assunto);
        Assert.AreEqual(Gravidade.Bloqueador, resultadoChamado.Gravidade);
        Assert.AreEqual("39815683039", resultadoChamado.CPF);
        Assert.AreEqual("maria.sharapova@gmail.com", resultadoChamado.Email);
        Assert.AreEqual("Preciso repor minha raquete de treinos com urgência.", resultadoChamado.Descricao);
    }

    [Test]
    public void TestaCriarChamadoComIdPreenchido()
    {
        //Criar objeto já existente dentro do Mock.
        var chamado = CriarChamado("6244c826eb6aeb6c5f44b0d0", "Limpesa das lentes", "James Cameron", Gravidade.Moderado, "82926196075",
            "james.cameron@gmail.com", "Precisamos limpar as câmeras semanalmente.");
        ChamadoController c = CriarController(true);
        IActionResult resultado = c.Inserir(chamado).Result;
        AfirmarBadRequest(resultado, "Não é possível inserir um chamado que já possui identificador.");
    }

    [Test]
    public void TestarPegarChamadosComPaginacao()
    {
        // O carregamento de dados cria 20 chamados no mock.
        ChamadoController c = CriarController(true);
        var resultado = c.PegarChamados(null, null).Result; // Valor padrão de 10 por página.
        var chamados = AfirmarOk<IList<Domain.Entities.Chamados>>(resultado, c);
        Assert.AreEqual(10, chamados.Count);

        resultado = c.PegarChamados(null, 50).Result;
        chamados = AfirmarOk<IList<Domain.Entities.Chamados>>(resultado, c);
        Assert.AreEqual(20, chamados.Count);

        resultado = c.PegarChamados(5, 50).Result;
        chamados = AfirmarOk<IList<Domain.Entities.Chamados>>(resultado, c);
        Assert.AreEqual(15, chamados.Count);

        resultado = c.PegarChamados(15, null).Result;
        chamados = AfirmarOk<IList<Domain.Entities.Chamados>>(resultado, c);
        Assert.AreEqual(5, chamados.Count);

        resultado = c.PegarChamados(20, null).Result;
        chamados = AfirmarOk<IList<Domain.Entities.Chamados>>(resultado, c);
        Assert.AreEqual(0, chamados.Count);
    }

    //[Test]
    //public void TestEndpointUpdateArticle()
    //{
    //    //ArticleController c = CreateController(true);
    //    //IActionResult result = c.Count();
    //    //long longResult = AssertOk<long>(result, 15);

    //    //result = c.Articles(4067); // Starlink Mission
    //    //Assert.IsInstanceOf<OkObjectResult>(result);
    //    //OkObjectResult okResult = (OkObjectResult)result;
    //    //Assert.IsInstanceOf<XArticle>(okResult.Value);
    //    //XArticle a = (XArticle)okResult.Value;
    //    //a.Title = a.Title += " [Updated]";
    //    //c.ArticlesPut(4067, a);
    //    //AssertStarlinkMission(a, "Starlink Mission [Updated]");

    //    //result = c.Count();
    //    //AssertOk<long>(result, longResult);
    //}

    //[Test]
    //public void TestEndpointUpdateArticleErrors()
    //{
    //    //var configuration = new MapperConfiguration(cfg =>
    //    //{
    //    //    cfg.CreateMap<XArticle, XArticle>();
    //    //});
    //    //Mapper m = new Mapper(configuration);
    //    //ArticleController c = CreateController(true);
    //    //IActionResult result = c.ArticlesPut(4067, null);
    //    //AssertError(result, "Invalid Article.");

    //    //result = c.Articles(4067); // Starlink Mission
    //    //Assert.IsInstanceOf<OkObjectResult>(result);
    //    //OkObjectResult okResult = (OkObjectResult)result;
    //    //Assert.IsInstanceOf<XArticle>(okResult.Value);
    //    //XArticle a = m.Map<XArticle>(okResult.Value);

    //    //result = c.ArticlesPut(154787985, a);
    //    //AssertNotFoundError(result, "Article 154787985 not found.");

    //    //a.Title = a.Url = a.ImageUrl = string.Empty;
    //    //result = c.ArticlesPut(4067, a);
    //    //AssertError(result, $"Title not informed.{Environment.NewLine}Url not informed.{Environment.NewLine}ImageUrl not informed.");

    //    //a.Title = "Starlink Mission";
    //    //result = c.ArticlesPut(4067, a);
    //    //AssertError(result, $"Url not informed.{Environment.NewLine}ImageUrl not informed.");

    //    //a.Url = "This is a Url content";
    //    //result = c.ArticlesPut(4067, a);
    //    //AssertError(result, $"ImageUrl not informed.");
    //}

    //[Test]
    //public void TestEndpointArticlesDeleteTrue()
    //{
    //    //ArticleController c = CreateController(true);
    //    //IActionResult result = c.Count();
    //    //long longResult = AssertOk<long>(result, 15);

    //    //result = c.ArticlesDelete(4067);
    //    //Assert.IsInstanceOf<NoContentResult>(result);

    //    //result = c.Count();
    //    //AssertOk<long>(result, longResult - 1);
    //}

    //[Test]
    //public void TestEndpointArticlesDeleteFalse()
    //{
    //    //ArticleController c = CreateController(true);
    //    //IActionResult result = c.Count();
    //    //long longResult = AssertOk<long>(result, 15);

    //    //result = c.ArticlesDelete(157849);
    //    //Assert.IsInstanceOf<NotFoundResult>(result);

    //    //result = c.Count();
    //    //AssertOk<long>(result, 15);
    //}

    //private long GetCount(ArticleController pController)
    //{
    //    IActionResult result = pController.Count();
    //    OkObjectResult okResult = (OkObjectResult)result;
    //    return (long)okResult.Value;
    //}

    private ChamadoController CriarController(bool pLoadData = false)
    {
        IChamadosRepositorio mock = new ChamadosRepositorioMock(pLoadData);
        IChamadosServico servico = new ChamadosServico(mock);
        return new ChamadoController(servico);
    }

    //public ArticleController AssertSearchPaginatedArticles(int? pStart, int? pLimit, int pAmount, ArticleController pController = null)
    //{
    //    ArticleController c = pController ?? CreateController(true); // Alimenta 15 artigos.
    //    IActionResult result = c.Articles(pLimit, pStart);

    //    Assert.IsInstanceOf<OkObjectResult>(result);
    //    OkObjectResult okResult = (OkObjectResult)result;
    //    Assert.IsInstanceOf<IEnumerable<XArticle>>(okResult.Value);
    //    IEnumerable<XArticle> enm = (IEnumerable<XArticle>)okResult.Value;
    //    XArticle[] resultArticles = enm.ToArray();
    //    Assert.AreEqual(pAmount, resultArticles.Length);
    //    return c;
    //}

    //private void AssertNotFoundError(IActionResult pResult, string pMessage)
    //{
    //    Assert.IsInstanceOf<NotFoundObjectResult>(pResult);
    //    NotFoundObjectResult errorResult = (NotFoundObjectResult)pResult;
    //    Assert.IsInstanceOf<string>(errorResult.Value);
    //    Assert.AreEqual(pMessage, errorResult.Value);
    //}

    private void AfirmarNotFound(IActionResult resultado, string mensagem)
    {
        Assert.IsInstanceOf<NotFoundObjectResult>(resultado);
        NotFoundObjectResult notFoundResultado = (NotFoundObjectResult)resultado;
        Assert.IsInstanceOf<string>(notFoundResultado.Value);
        Assert.AreEqual(mensagem, notFoundResultado.Value);
    }

    private void AfirmarBadRequest(IActionResult resultado, string mensagem)
    {
        Assert.IsInstanceOf<BadRequestObjectResult>(resultado);
        BadRequestObjectResult errorResult = (BadRequestObjectResult)resultado;
        Assert.IsInstanceOf<string>(errorResult.Value);
        Assert.AreEqual(mensagem, errorResult.Value);
    }

    private T AfirmarOk<T>(IActionResult resultado, object valor)
    {
        Assert.IsInstanceOf<OkObjectResult>(resultado);
        OkObjectResult okResult = (OkObjectResult)resultado;
        Assert.IsInstanceOf<T>(okResult.Value);
        return (T)okResult.Value;
    }

    private T AfirmarOkCriado<T>(IActionResult resultado, object valor)
    {
        Assert.IsInstanceOf<CreatedAtActionResult>(resultado);
        CreatedAtActionResult resultadoOkCriado = (CreatedAtActionResult)resultado;
        Assert.IsInstanceOf<T>(resultadoOkCriado.Value);
        return (T)resultadoOkCriado.Value;
    }

    //private void AssertStarlinkMission(XArticle pArticle, string pTitle = "Starlink Mission")
    //{
    //    Assert.AreEqual(4067, pArticle.ID);
    //    Assert.AreEqual(false, pArticle.Featured);
    //    Assert.AreEqual(pTitle, pArticle.Title);
    //    Assert.AreEqual("https://www.spacex.com/news/2020/01/07/starlink-mission", pArticle.Url);
    //    Assert.AreEqual("https://www.spacex.com/sites/spacex/files/styles/featured_news_widget_image/public/field/image/starlink_2_outtower_website.jpg?itok=-i8nhHqy", pArticle.ImageUrl);
    //    Assert.AreEqual("SpaceX", pArticle.NewsSite);
    //    Assert.AreEqual(string.Empty, pArticle.Summary);
    //    Assert.AreEqual("2020-01-07T00:00:00.000Z", pArticle.PublishedAt);
    //    Assert.AreEqual("2021-05-18T13:45:49.196Z", pArticle.UpdatedAt);
    //    Assert.AreEqual(0, pArticle.Launches.Length);
    //    Assert.AreEqual(0, pArticle.Events.Length);
    //}

    private Domain.Entities.Chamados CriarChamado(string id, string nomePessoa, string assunto, Gravidade gravidade, string cpf, string email, string descricao)
    {
        var c = new Domain.Entities.Chamados();
        c.Id = id;
        c.Aberto = true;
        c.NomePessoa = nomePessoa;
        c.Gravidade = gravidade;
        c.Assunto = assunto;
        c.CPF = cpf;
        c.Email = email;
        c.Descricao = descricao;
        return c;
    }

    //private XArticle CreateArticle(int? pID, string pTitle = "James Webb reach lagrange point 2", string pUrl = "This is a Url content", string pImageUrl = "This is a ImageUrl content")
    //{
    //    ArticleController c = CreateController();
    //    long count = GetCount(c);

    //    XArticle a = CreateArticleObject(pID, pTitle, pUrl, pImageUrl);

    //    IActionResult result = c.Articles(a);
    //    XArticle newArticle = AssertOkCreated<XArticle>(result, a);
    //    CreatedAtActionResult resultCreated = (CreatedAtActionResult)result;
    //    Assert.AreEqual("articles", resultCreated.ActionName);
    //    Assert.AreEqual(1, resultCreated.RouteValues.Count);
    //    Assert.AreEqual("id", resultCreated.RouteValues.First().Key);
    //    Assert.AreEqual(a.ID, resultCreated.RouteValues.First().Value);
    //    Assert.IsFalse(newArticle.Featured);
    //    Assert.IsTrue(string.IsNullOrEmpty(a.ObjectID));
    //    Assert.AreSame(a, newArticle);
    //    Assert.AreEqual(pTitle, newArticle.Title);
    //    Assert.AreEqual("On 24 January, 30 days after launch on Christmas Day, the James Webb Space Telescope...", newArticle.Summary);
    //    Assert.AreEqual(pUrl, newArticle.Url);
    //    Assert.AreEqual(pImageUrl, newArticle.ImageUrl);
    //    long newCount = GetCount(c);
    //    Assert.AreEqual(count + 1, newCount);
    //    return newArticle;
    //}
}