using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RL.Backend.Commands;
using RL.Data;
using RL.Data.DataModels;
using RL.Data.DTOs;

namespace RL.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanProcedureController : ControllerBase
{
    private readonly ILogger<PlanProcedureController> _logger;
    private readonly RLContext _context;
    private readonly IMediator _mediator;

    public PlanProcedureController(ILogger<PlanProcedureController> logger, RLContext context, IMediator mediator)
    {
        _logger = logger;
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [EnableQuery]
    public IEnumerable<PlanProcedure> Get()
    {
        return _context.PlanProcedures;
    }


    [HttpPost("User")]
    public async Task<ActionResult<PlanProcedureUserDto>> AddUserToPlanProcedure(AddUserToPlanProcedureCommand command, CancellationToken token)
    {
        try
        {
            var response = await _mediator.Send(command, token);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            _logger.LogError("Failed to create PlanProcedureUser. Reason: {Message}", response.Value);

            return BadRequest(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("RemoveUserFromPlanProcedure")]
    public async Task<ActionResult<PlanProcedureUserDto>> RemoveUserFromPlanProcedure(DeleteUserToPlanProcedureCommand command, CancellationToken token)
    {
        try
        {
            var response = await _mediator.Send(command, token);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            _logger.LogError("Failed to create PlanProcedureUser. Reason: {Message}", response.Value);

            return BadRequest(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("RemoveAllUsersFromPlanProcedure")]
    public async Task<ActionResult<PlanProcedureUserDto>> RemoveAllUsersFromPlanProcedure(DeleteAllUsersToPlanProcedureCommand command, CancellationToken token)
    {
        try
        {
            var response = await _mediator.Send(command, token);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            _logger.LogError("Failed to create PlanProcedureUser. Reason: {Message}", response.Value);

            return BadRequest(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred");
            return StatusCode(500, ex.Message);
        }
    }
}
