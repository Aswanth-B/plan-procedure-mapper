using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.Data.DTOs
{
    public class PlanProcedureUserDto
    {
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public int ProcedureId { get; set; }
    }
}
