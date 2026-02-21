using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTracker.Domain
{
    public class TaskAssignment
    {
        public int Id { get; set; }
        public string ColleagueId { get; set; } = null!;
        public DateTimeOffset ShiftStart { get; set; }
        public DateTimeOffset ShiftEnd { get; set; }
    }
}
