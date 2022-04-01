using Chamados.Service.Toolkit.Web;

namespace Chamados.Service.Api.Controllers;

[ApiController]
[Route("chamados")]
public class ChamadoController : ManagedController
{
    private readonly IServico<Domain.Entidades.Chamados, string> _Servico;

    public ChamadoController(IServico<Domain.Entidades.Chamados, string> servico)
    {
        _Servico = servico;
    }

    /// <summary>Retorna o total de chamados que existem.</summary>
    [HttpGet("contador")]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PegarQuantidade()
    {
        return await TryExecuteOK(async () => await _Servico.PegarQuantidadeAsync());
    }

    /// <summary>Retorna os chamados com possiblidade de paginação.</summary>
    /// <param name="inicio">Ignora um especificado número de chamados. Esse recurso é especialmente útil para paginação.</param>
    /// <param name="limite">Número máximo de chamados retornados pelo serviço (Limitado a 50 registros).</param>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Domain.Entidades.Chamados>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PegarChamados(int? inicio, int? limite)
    {
        return await TryExecuteOK(async () => await _Servico.PegarAsync(inicio, limite));
    }

    /// <summary>Retorna um chamado pelo seu identificador.</summary>
    /// <param name="id">Identificador do artigo.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Domain.Entidades.Chamados), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PegarChamadoPorId(string id)
    {
        return await TryExecuteOK(async () => await _Servico.PegarPorIdAsync(id));
    }

    /// <summary>Excluir um chamado existente</summary>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Excluir(string id)
    {
        return await TryExecuteDelete(async () => await _Servico.ExcluirAsync(id));
    }

    /// <summary>Atualizar um chamado existente</summary>
    [HttpPut]
    [ProducesResponseType(typeof(Domain.Entidades.Chamados), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Atualizar([FromBody] Domain.Entidades.Chamados chamado)
    {
        return await TryExecuteOK(async () => await _Servico.AtualizarAsync(chamado));
    }

    /// <summary>Criar um novo chamado</summary>
    [HttpPost]
    [ProducesResponseType(typeof(Domain.Entidades.Chamados), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Inserir([FromBody] Domain.Entidades.Chamados chamado)
    {
        Func<Task<object>> execute = async delegate
        {
            return await _Servico.InserirAsync(chamado);
        };
        Func<object, IActionResult> action = delegate (object result)
        {
            Domain.Entidades.Chamados c = result as Domain.Entidades.Chamados;
            return CreatedAtAction(nameof(PegarChamadoPorId).ToLower(), new { id = c.Id }, result);
        };
        return await TryExecute(action, execute);
    }
}