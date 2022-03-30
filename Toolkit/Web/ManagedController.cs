using Microsoft.AspNetCore.Mvc;
using Chamados.Service.Toolkit.Exceptions;

namespace Chamados.Service.Toolkit.Web;

public abstract class ManagedController : ControllerBase
{
    protected async Task<IActionResult> TryExecuteOK(Func<Task<object>> pExecute)
    {
        Func<object, IActionResult> action = delegate (object result)
       {
           return Ok(result);
       };
        return await TryExecute(action, pExecute);
    }

    protected async Task<IActionResult> TryExecuteDelete(Func<Task<object>> pExecute)
    {
        Func<object, IActionResult> action = delegate (object result)
        {
            bool sucess = (bool)result;
            return sucess ? NoContent() : NotFound();
        };
        return await TryExecute(action, pExecute);
    }

    protected async Task<IActionResult> TryExecute(Func<object, IActionResult> pResultFunc, Func<Task<object>> pExecute)
    {
        try
        {
            object result = await pExecute();
            return pResultFunc(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ForbidException ex)
        {
            return Forbid(ex.Message);
        }
        catch (UnauthorizedException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}