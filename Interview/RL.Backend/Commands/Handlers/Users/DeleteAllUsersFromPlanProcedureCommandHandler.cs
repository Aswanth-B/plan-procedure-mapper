using MediatR;
using Microsoft.EntityFrameworkCore;
using RL.Backend.Exceptions;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;
using RL.Data.DTOs;

namespace RL.Backend.Commands.Handlers.Users
{
    public class DeleteAllUsersFromPlanProcedureCommandHandler : IRequestHandler<DeleteAllUsersToPlanProcedureCommand, ApiResponse<Unit>> 
    {
        private readonly RLContext _context;
        public DeleteAllUsersFromPlanProcedureCommandHandler(RLContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<Unit>> Handle(DeleteAllUsersToPlanProcedureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var planProcedureUsers = await _context.PlanProcedureUsers.Where(p => p.ProcedureId == request.ProcedureId && p.PlanId == request.PlanId).ToListAsync();

                if (planProcedureUsers is null)
                    throw new Exception($"ProcedureId: {request.ProcedureId} or PlanId: {request.PlanId} not found");


                _context.PlanProcedureUsers.RemoveRange(planProcedureUsers);

                await _context.SaveChangesAsync();

                return ApiResponse<Unit>.Succeed(new Unit());
            }

            catch (Exception ex) {
                return ApiResponse<Unit>.Fail(ex);
            }

        }
    }
}
