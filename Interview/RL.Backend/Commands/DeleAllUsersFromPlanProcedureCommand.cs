using MediatR;
using RL.Backend.Models;
using RL.Data.DTOs;

namespace RL.Backend.Commands
{
    public class DeleteAllUsersToPlanProcedureCommand : IRequest<ApiResponse<Unit>>
    {
        public int PlanId { get; set; }
        public int ProcedureId { get; set; }
    }
}
