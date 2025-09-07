using MediatR;
using Microsoft.EntityFrameworkCore;
using RL.Backend.Exceptions;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;
using RL.Data.DTOs;
using System.Numerics;

namespace RL.Backend.Commands.Handlers.Users
{
    public class AddUserToPlanProcedureCommandHandler: IRequestHandler<AddUserToPlanProcedureCommand, ApiResponse<PlanProcedureUserDto>>
    {
        private readonly RLContext _context;
        public AddUserToPlanProcedureCommandHandler(RLContext context) 
        { 
            _context = context;
        }

        public async Task<ApiResponse<PlanProcedureUserDto>>  Handle(AddUserToPlanProcedureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var planProcedure = await _context.PlanProcedures.Include(x => x.PlanProcedureUsers).FirstOrDefaultAsync(pp => pp.ProcedureId == request.ProcedureId && pp.PlanId == request.PlanId, cancellationToken);

                if (planProcedure is null)
                    return ApiResponse<PlanProcedureUserDto>.Fail(new NotFoundException($"ProcedureId: {request.ProcedureId} or PlanId: {request.PlanId} not found"));

                //Already has the procedure, so just succeed
                if (planProcedure.PlanProcedureUsers.Any(ppu => ppu.ProcedureId == request.ProcedureId && ppu.PlanId == request.PlanId && ppu.UserId == request.UserId))
                    return ApiResponse<PlanProcedureUserDto>.Succeed(new PlanProcedureUserDto());

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

                return ApiResponse<PlanProcedureUserDto>.Succeed(dto);
            }
            catch (Exception e)
            {
                return ApiResponse<PlanProcedureUserDto>.Fail(e);
            }
        }
    }
}
