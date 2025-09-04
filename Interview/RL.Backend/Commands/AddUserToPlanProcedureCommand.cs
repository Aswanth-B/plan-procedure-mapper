using MediatR;
using RL.Backend.Models;
using RL.Data.DTOs;

namespace RL.Backend.Commands
{
    public class AddUserToPlanProcedureCommand : IRequest<PlanProcedureUserDto>
    {
        public int PlanId { get; set; }
        public int ProcedureId { get; set; }
        public int UserId { get; set; }
    }
}
