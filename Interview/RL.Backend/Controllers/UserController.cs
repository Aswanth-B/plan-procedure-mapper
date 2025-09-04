using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OData.UriParser;
using RL.Backend.Commands;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;
using RL.Data.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RL.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly RLContext _context;
    private readonly IMediator _mediator;

    public UsersController(ILogger<UsersController> logger, RLContext context, IMediator mediator)
    {
        _logger = logger;
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [EnableQuery]
    public IEnumerable<User> Get()
    {
        return _context.Users;
    }

    [HttpPost("AddUserToPlanProcedure")]
    public async Task<ActionResult<PlanProcedureUserDto>> AddUserToPlanProcedure(AddUserToPlanProcedureCommand command, CancellationToken token)
    {
        var response = await _mediator.Send(command, token);

        return Ok(response);
    }

    [HttpDelete("RemoveUserFromPlanProcedure")]
    public async Task<ActionResult<PlanProcedureUserDto>> RemoveUserFromPlanProcedureAddUserToPlanProcedure(DeleteUserToPlanProcedureCommand command, CancellationToken token)
    {
        var response = await _mediator.Send(command, token);

        return Ok(response);
    }

    [HttpDelete("RemoveAllUsersFromPlanProcedure")]
    public async Task<ActionResult<PlanProcedureUserDto>> RemoveAllUsersFromPlanProcedureAddUserToPlanProcedure(DeleteAllUsersToPlanProcedureCommand command, CancellationToken token)
    {
        var response = await _mediator.Send(command, token);

        return Ok(response);
    }
}
