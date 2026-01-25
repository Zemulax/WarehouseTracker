using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTracker.Domain
{
    public class ShiftAssignment
    {
        public int Id { get; set; }
        public int ColleagueId { get; set; }
        public int DepartmentId { get; set; }
        public DateOnly ShiftDate { get; set; }
        public TimeOnly ShiftStart { get; set; }
        public TimeOnly ShiftEnd { get; set; }
    }
}
