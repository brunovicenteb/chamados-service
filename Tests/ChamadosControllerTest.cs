using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Chamados.Service.Tests.Mock;
using Chamados.Service.Application;
using Chamados.Service.Api.Controllers;
using Chamados.Service.Domain.Interfaces.Servicos;
using Chamados.Service.Domain.Interfaces.Repositorios;
using Chamados.Service.Domain.Enums;

namespace Chamados.Service.Tests;
public class ArticleControllerTest
{
    [Test]
    public void TestaPegarQuantidade()
    {
        ChamadoController c = CriarController(true);
        IActionResult result = c.PegarQuantidade(false).Result;
        long longResult = AfirmarOk<long>(result, 4);
        Assert.AreEqual(longResult, 4);
    }

    [Test]
    public void TestaPegarQuantidadeIncluindoChamadosFechados()
    {
        ChamadoController c = CriarController(true);
        IActionResult result = c.PegarQuantidade(true).Result;
        long longResult = AfirmarOk<long>(result, 5);
        Assert.AreEqual(longResult, 5);
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
        Assert.AreEqual("Maria Sharapova", chamado.NomePessoa);
        Assert.AreEqual("Substituição de Raquete", chamado.Assunto);
        Assert.AreEqual(Gravidade.Bloqueador, chamado.Gravidade);
        Assert.AreEqual("39815683039", chamado.CPF);
        Assert.AreEqual("maria.sharapova@gmail.com", chamado.Email);
        Assert.AreEqual("Preciso repor minha raquete de treinos com urgência.", chamado.Descricao);
    }

    [Test]
    public void TestaCriarChamadoComIdPreenchido()
    {
        //Criar objeto já existente dentro do Mock.
        var chamado = CriarChamado("6244c826eb6aeb6c5f44b0d0", "Limpesa das lentes", "James Cameron", Gravidade.Moderado, "82926196075",
            "james.cameron@gmail.com", "Precisamos limpar as câmeras semanalmente.");
        ChamadoController c = CriarController(true);
        IActionResult resultado = c.Inserir(chamado).Result;
        AfirmarErro(resultado, "Não é possível inserir um chamado que já possui identificador.");
    }

    //[Test]
    //public void TestEndpointCreateArticleWithID()
    //{
    //    //XArticle a = CreateArticle(15788745);
    //    //Assert.AreEqual(15788745, a.ID);
    //}

    //[Test]
    //public void TestEndpointCreateArticleWithErrors()
    //{
    //    //ArticleController c = CreateController();
    //    //IActionResult result = c.Articles(null);
    //    //AssertError(result, "Invalid Article.");

    //    //XArticle a = CreateArticleObject(5789654, null, null, null);
    //    //result = c.Articles(a);
    //    //AssertError(result, $"Title not informed.{Environment.NewLine}Url not informed.{Environment.NewLine}ImageUrl not informed.");

    //    //a.Title = "Starlink Mission";
    //    //result = c.Articles(a);
    //    //AssertError(result, $"Url not informed.{Environment.NewLine}ImageUrl not informed.");

    //    //a.Url = "This is a Url content";
    //    //result = c.Articles(a);
    //    //AssertError(result, $"ImageUrl not informed.");
    //}

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
    //public void TestEndpointGetArticleByID()
    //{
    //    //ArticleController c = CreateController(true);
    //    //IActionResult result = c.Articles(4067);
    //    //Assert.IsInstanceOf<OkObjectResult>(result);
    //    //OkObjectResult okResult = (OkObjectResult)result;
    //    //Assert.IsInstanceOf<XArticle>(okResult.Value);
    //    //XArticle a = (XArticle)okResult.Value;
    //    //AssertStarlinkMission(a);
    //}

    //[Test]
    //public void TestEndpointGetArticleByIDError()
    //{
    //    //ArticleController c = CreateController(true);
    //    //IActionResult result = c.Articles(989857457);
    //    //AssertNotFoundError(result, "Article 989857457 not found.");
    //}

    //[Test]
    //public void TestEndpointSearchArticleEmpty()
    //{
    //    //ArticleController c = CreateController();
    //    //IActionResult result = c.Articles();
    //    //Assert.IsInstanceOf<OkObjectResult>(result);
    //    //OkObjectResult okResult = (OkObjectResult)result;
    //    //Assert.IsInstanceOf<IEnumerable<XArticle>>(okResult.Value);
    //    //IEnumerable<XArticle> enm = (IEnumerable<XArticle>)okResult.Value;
    //    //XArticle[] resultArticles = enm.ToArray();
    //    //Assert.AreEqual(0, resultArticles.Length);
    //}

    //[Test]
    //public void TestEndpointSearchPaginatedArticles()
    //{
    //    //// Testa paginação retornando o valor limite padrão de 10 artigos
    //    //ArticleController c = AssertSearchPaginatedArticles(null, null, 10);

    //    //// Testar pular mais do que o total de 5 em 5 artigos
    //    //AssertSearchPaginatedArticles(16, null, 0);

    //    //// Testar paginação de 5 em 5 artigos
    //    //AssertSearchPaginatedArticles(null, 5, 5);
    //    //AssertSearchPaginatedArticles(5, 5, 5);
    //    //AssertSearchPaginatedArticles(10, 5, 5);
    //    //AssertSearchPaginatedArticles(15, 5, 0);

    //    //// Testar paginação de 10 em 10 artigos
    //    //AssertSearchPaginatedArticles(null, 10, 10);
    //    //AssertSearchPaginatedArticles(10, 10, 5);
    //    //AssertSearchPaginatedArticles(15, 5, 0);
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

    private void AfirmarErro(IActionResult resultado, string mensagem)
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