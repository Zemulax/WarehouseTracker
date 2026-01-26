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
        public int ColleagueId { get; set; }
        public int DepartmentId { get; set; }
        public int ShiftAssignmentId { get; set; }
        public string SessionType { get; set; } = null!;
        public TimeOnly SessionStart { get; set; }
        public TimeOnly SessionEnd { get; set; }
    }
}
