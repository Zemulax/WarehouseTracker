using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTracker.Domain
{
    public class ActivitySession
    {
        public int Id { get; set; }
        public string ColleagueId { get; set; } = null!;
        public int?  DepartmentId { get; set; }
        public int ShiftAssignmentId { get; set; }
        public string SessionType { get; set; } = null!;
        public DateTimeOffset SessionStart { get; set; }
        public DateTimeOffset SessionEnd { get; set; }
    }
}
