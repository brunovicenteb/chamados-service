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
    private const string _HermioneGrangerId = "6244c826eb6aeb6c5f44b0d0";

    [Test]
    public void TestaPegarQuantidade()
    {
        ChamadoController c = CriarController(true);
        IActionResult result = c.PegarQuantidade(true).Result;
        long longResult = AfirmarOk<long>(result);
        Assert.AreEqual(longResult, 20);
    }

    [Test]
    public void TestaPegarQuantidadeSemChamadosFechados()
    {
        ChamadoController c = CriarController(true);
        IActionResult result = c.PegarQuantidade(false).Result;
        long longResult = AfirmarOk<long>(result);
        Assert.AreEqual(16, longResult);
    }

    [Test]
    public void TestaPegarChamadoPorId()
    {
        ChamadoController c = CriarController(true);
        IActionResult resultado = c.PegarChamadoPorId(_JamesCameronId).Result;
        var chamado = AfirmarOk<Domain.Entities.Chamados>(resultado);
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
        AfirmarNotFound(resultado, "N�o foi poss�vel encontrar o chamado com o identificador informado.");
    }

    [Test]
    public void TestaPegarChamadoPorIdSemPassarId()
    {
        ChamadoController c = CriarController(true);
        IActionResult resultado = c.PegarChamadoPorId(string.Empty).Result;
        AfirmarBadRequest(resultado, "N�o � poss�vel encontrar um chamado sem um identificador.");
    }

    [Test]
    public void TestaCriarChamado()
    {
        ChamadoController c = CriarController(false);
        IActionResult resultQuantidade = c.PegarQuantidade(true).Result;
        long longResult = AfirmarOk<long>(resultQuantidade);
        Assert.AreEqual(longResult, 0);

        var chamado = CriarChamado(string.Empty, "Maria Sharapova", "Substitui��o de Raquete", Gravidade.Bloqueador, "39815683039",
            "maria.sharapova@gmail.com", "Preciso repor minha raquete de treinos com urg�ncia.");
        IActionResult resultado = c.Inserir(chamado).Result;
        var resultadoChamado = AfirmarOkCriado<Domain.Entities.Chamados>(resultado, c);
        Assert.IsFalse(string.IsNullOrEmpty(resultadoChamado.Id));
        Assert.IsTrue(resultadoChamado.Aberto);
        Assert.AreEqual("Maria Sharapova", resultadoChamado.NomePessoa);
        Assert.AreEqual("Substitui��o de Raquete", resultadoChamado.Assunto);
        Assert.AreEqual(Gravidade.Bloqueador, resultadoChamado.Gravidade);
        Assert.AreEqual("39815683039", resultadoChamado.CPF);
        Assert.AreEqual("maria.sharapova@gmail.com", resultadoChamado.Email);
        Assert.AreEqual("Preciso repor minha raquete de treinos com urg�ncia.", resultadoChamado.Descricao);

        resultQuantidade = c.PegarQuantidade(true).Result;
        longResult = AfirmarOk<long>(resultQuantidade);
        Assert.AreEqual(longResult, 1);
    }

    [Test]
    public void TestaCriarChamadoComIdPreenchido()
    {
        //Criar objeto com id que j� existente dentro do Mock.
        var chamado = CriarChamado("6244c826eb6aeb6c5f44b0d0", "Limpesa das lentes", "James Cameron", Gravidade.Moderado, "82926196075",
            "james.cameron@gmail.com", "Precisamos limpar as c�meras semanalmente.");
        ChamadoController c = CriarController(true);
        IActionResult resultado = c.Inserir(chamado).Result;
        AfirmarBadRequest(resultado, "N�o � poss�vel inserir um chamado que j� possui identificador.");
    }

    [Test]
    public void TestaAtualizarChamado()
    {
        ChamadoController c = CriarController(true);
        var resultadoChamadoOriginal = c.PegarChamadoPorId(_HermioneGrangerId).Result;
        var chamadoOriginal = AfirmarOk<Domain.Entities.Chamados>(resultadoChamadoOriginal);
        Assert.AreEqual("Hermione Granger", chamadoOriginal.NomePessoa);
        Assert.AreEqual("hermione.granger@gmail.com", chamadoOriginal.Email);
        Assert.AreEqual("Resultado de Testes", chamadoOriginal.Assunto);
        Assert.AreEqual("Aguardo resultados de testes.", chamadoOriginal.Descricao);

        IActionResult resultQuantidade = c.PegarQuantidade(true).Result;
        long longResult = AfirmarOk<long>(resultQuantidade);
        Assert.AreEqual(longResult, 20);

        var chamadoAtualizado = CriarChamado(_HermioneGrangerId, "Hermione Granger", "Resultado de Testes Atualizados", Gravidade.Bloqueador, "23257183283",
            "hermione.granger@gmail.com", "Aguardo resultados de testes atualizados.");
        var resultadoChamadoAtualizado = c.Atualizar(chamadoAtualizado).Result;
        chamadoAtualizado = AfirmarOk<Domain.Entities.Chamados>(resultadoChamadoAtualizado);
        Assert.AreEqual("Hermione Granger", chamadoAtualizado.NomePessoa);
        Assert.AreEqual("hermione.granger@gmail.com", chamadoAtualizado.Email);
        Assert.AreEqual("Resultado de Testes Atualizados", chamadoAtualizado.Assunto);
        Assert.AreEqual("Aguardo resultados de testes atualizados.", chamadoAtualizado.Descricao);

        resultQuantidade = c.PegarQuantidade(true).Result;
        longResult = AfirmarOk<long>(resultQuantidade);
        Assert.AreEqual(longResult, 20);
    }

    [Test]
    public void TestaAtualizarChamadoSemIdPreenchido()
    {
        var chamado = CriarChamado(string.Empty, "Limpesa das lentes", "James Cameron", Gravidade.Moderado, "82926196075",
            "james.cameron@gmail.com", "Precisamos limpar as c�meras semanalmente.");
        ChamadoController c = CriarController(true);
        IActionResult resultado = c.Atualizar(chamado).Result;
        AfirmarBadRequest(resultado, "N�o � poss�vel atualizar um chamado sem um identificador.");
    }

    [Test]
    public void TestaAtualizarChamadoComIdPreenchido()
    {
        var chamado = CriarChamado(Guid.NewGuid().ToString(), "Limpesa das lentes", "James Cameron", Gravidade.Moderado, "82926196075",
            "james.cameron@gmail.com", "Precisamos limpar as c�meras semanalmente.");
        ChamadoController c = CriarController(true);
        IActionResult resultado = c.Atualizar(chamado).Result;
        AfirmarNotFound(resultado, "N�o foi poss�vel atualizar o chamado com o identificador informado.");
    }

    [Test]
    public void TestaExcluirChamado()
    {
        ChamadoController c = CriarController(true);
        IActionResult resultQuantidade = c.PegarQuantidade(true).Result;
        long longResult = AfirmarOk<long>(resultQuantidade);
        Assert.AreEqual(longResult, 20);

        IActionResult resultado = c.Excluir(_JamesCameronId).Result;
        Assert.IsInstanceOf<NoContentResult>(resultado);

        resultQuantidade = c.PegarQuantidade(true).Result;
        longResult = AfirmarOk<long>(resultQuantidade);
        Assert.AreEqual(longResult, 19);
    }

    [Test]
    public void TestaExcluirChamadoSemIdPreenchido()
    {
        ChamadoController c = CriarController(true);
        IActionResult resultado = c.Excluir(string.Empty).Result;
        AfirmarBadRequest(resultado, "N�o � poss�vel excluir um chamado sem um identificador.");
    }

    [Test]
    public void TestaExcluirChamadoComIdInexistente()
    {
        ChamadoController c = CriarController(true);
        IActionResult resultado = c.Excluir(Guid.NewGuid().ToString()).Result;
        AfirmarNotFound(resultado, "N�o foi poss�vel excluir o chamado com o identificador informado.");
    }

    [Test]
    public void TestarPegarChamadosComPaginacao()
    {
        // O carregamento de dados cria 20 chamados no mock.
        ChamadoController c = CriarController(true);
        var resultado = c.PegarChamados(null, null).Result; // Valor padr�o de 10 por p�gina.
        var chamados = AfirmarOk<IList<Domain.Entities.Chamados>>(resultado);
        Assert.AreEqual(10, chamados.Count);

        resultado = c.PegarChamados(null, 50).Result;
        chamados = AfirmarOk<IList<Domain.Entities.Chamados>>(resultado);
        Assert.AreEqual(20, chamados.Count);

        resultado = c.PegarChamados(5, 50).Result;
        chamados = AfirmarOk<IList<Domain.Entities.Chamados>>(resultado);
        Assert.AreEqual(15, chamados.Count);

        resultado = c.PegarChamados(15, null).Result;
        chamados = AfirmarOk<IList<Domain.Entities.Chamados>>(resultado);
        Assert.AreEqual(5, chamados.Count);

        resultado = c.PegarChamados(20, null).Result;
        chamados = AfirmarOk<IList<Domain.Entities.Chamados>>(resultado);
        Assert.AreEqual(0, chamados.Count);
    }

    private ChamadoController CriarController(bool pLoadData = false)
    {
        IChamadosRepositorio mock = new ChamadosRepositorioMock(pLoadData);
        IChamadosServico servico = new ChamadosServico(mock);
        return new ChamadoController(servico);
    }

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

    private T AfirmarOk<T>(IActionResult resultado)
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
}