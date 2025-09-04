using MediatR;
using Microsoft.EntityFrameworkCore;
using RL.Backend.Exceptions;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;
using RL.Data.DTOs;

namespace RL.Backend.Commands.Handlers.Users
{
    public class AddUserToPlanProcedureCommandHandler: IRequestHandler<AddUserToPlanProcedureCommand, PlanProcedureUserDto>
    {
        private readonly RLContext _context;
        public AddUserToPlanProcedureCommandHandler(RLContext context) 
        { 
            _context = context;
        }

        public async Task<PlanProcedureUserDto> Handle(AddUserToPlanProcedureCommand request, CancellationToken cancellationToken)
        {
            var planProcedure = await _context.PlanProcedures.Include(x => x.PlanProcedureUsers).FirstOrDefaultAsync(pp => pp.ProcedureId == request.ProcedureId && pp.PlanId == request.PlanId, cancellationToken);

            if (planProcedure is null)
                throw new Exception($"ProcedureId: {request.ProcedureId} or PlanId: {request.PlanId} not found");

            var newUser = new PlanProcedureUser
            {
                UserId = request.UserId
            };

            planProcedure.PlanProcedureUsers.Add(newUser);

            await _context.SaveChangesAsync(cancellationToken);

            var dto = new PlanProcedureUserDto
            {
                PlanId = newUser.PlanId,
                ProcedureId = newUser.ProcedureId,
                UserId = newUser.UserId
            };

            return dto;
            
        }
    }
}
