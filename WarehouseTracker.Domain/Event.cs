using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTracker.Domain
{
    public class Event
    {
        public int Id { get; set; }

        public string ColleagueId { get; set; } = null!;

        public string DepartmentCode { get; set; } = null!;

        public int ShiftAssignmentId { get; set; }

        public string EventType { get; set; } = null!;

        public DateTime TimestampUtc { get; set; }

        public string Source { get; set; } = "User";
    }
}
