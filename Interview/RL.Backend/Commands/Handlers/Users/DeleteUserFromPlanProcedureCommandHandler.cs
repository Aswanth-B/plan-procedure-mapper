using MediatR;
using Microsoft.EntityFrameworkCore;
using RL.Backend.Exceptions;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;
using RL.Data.DTOs;

namespace RL.Backend.Commands.Handlers.Users
{
    public class DeleteUserFromPlanProcedureCommandHandler : IRequestHandler<DeleteUserToPlanProcedureCommand>
    {
        private readonly RLContext _context;
        public DeleteUserFromPlanProcedureCommandHandler(RLContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteUserToPlanProcedureCommand request, CancellationToken cancellationToken)
        {
            var planProcedureUser = await _context.PlanProcedureUsers.FirstOrDefaultAsync(p => p.ProcedureId == request.ProcedureId && p.PlanId == request.PlanId && p.UserId == request.UserId);

            if (planProcedureUser is null)
                throw new Exception($"ProcedureId: {request.ProcedureId} or PlanId: {request.PlanId} not found");


            _context.PlanProcedureUsers.Remove(planProcedureUser);

            await _context.SaveChangesAsync();

            return Unit.Value;

        }
    }
}
