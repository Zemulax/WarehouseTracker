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
        public int WorkDayId { get; set; }
        public WorkDay WorkDay { get; set; } = null!;
        public string ColleagueId { get; set; } = null!;
        public string DepartmentCode { get; set; } = null!; // pick, pack, etc
        public DateTimeOffset TaskStart { get; set; }
        public DateTimeOffset? TaskEnd { get; set; }
        public string Status { get; set; } = string.Empty; // Active, Completed, Missed
    }
}
