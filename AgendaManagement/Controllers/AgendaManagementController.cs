using Application.Dto.Citas.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AgendaManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgendaManagementController : ControllerBase
{
    private readonly IMediator _mediator;

    public AgendaManagementController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet(nameof(GetCitaByDay))]
    public async Task<ActionResult<string>> GetCitaByDay([FromQuery] GetCitaByDayRequestDto request)
    {
        try
        {
            var result = await _mediator
                .Send(request)
                .ConfigureAwait(false);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
