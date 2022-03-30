using Chamados.Service.Toolkit.Web;

namespace Chamados.Service.Api.Controllers;

[ApiController]
[Route("chamados")]
public class ChamadoController : ManagedController
{
    private readonly IChamadosServico _ChamadoServico;

    public ChamadoController(IChamadosServico chamadoServico)
    {
        _ChamadoServico = chamadoServico;
    }

    /// <summary>Retorna o total de chamados que existem.</summary>
    [HttpGet("contador")]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PegarQuantidade(bool apenasAbertos)
    {
        return await TryExecuteOK(async () => await _ChamadoServico.PegarQuantidadeAsync(apenasAbertos));
    }

    /// <summary>Retorna os chamados com possiblidade de paginação.</summary>
    /// <param name="inicio">Ignora um especificado número de chamados. Esse recurso é especialmente útil para paginação.</param>
    /// <param name="limite">Número máximo de chamados retornados pelo serviço (Limitado a 50 registros).</param>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Domain.Entities.Chamados>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PegarChamados(int? inicio, int? limite)
    {
        return await TryExecuteOK(async () => await _ChamadoServico.PegarChamadosAsync(inicio, limite));
    }

    /// <summary>Retorna um chamado pelo seu identificador.</summary>
    /// <param name="id">Identificador do artigo.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Domain.Entities.Chamados), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PegarChamadoPorId(string id)
    {
        return await TryExecuteOK(async () => await _ChamadoServico.PegarChamadoPorIdAsync(id));
    }

    /// <summary>Atualizar novo chamado existente</summary>
    [HttpPut]
    [ProducesResponseType(typeof(Domain.Entities.Chamados), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Atualizar([FromBody] Domain.Entities.Chamados chamado)
    {
        return await TryExecuteOK(async () => await _ChamadoServico.AtualizarAsync(chamado));
    }

    /// <summary>Salva um novo chamado</summary>
    [HttpPost]
    [ProducesResponseType(typeof(Domain.Entities.Chamados), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Inserir([FromBody] Domain.Entities.Chamados chamado)
    {
        Func<Task<object>> execute = async delegate
        {
            return await _ChamadoServico.InserirAsync(chamado);
        };
        Func<object, IActionResult> action = delegate (object result)
        {
            Domain.Entities.Chamados c = result as Domain.Entities.Chamados;
            return CreatedAtAction(nameof(PegarChamadoPorId).ToLower(), new { id = c.ObjectID }, result);
        };
        return await TryExecute(action, execute);
    }
}