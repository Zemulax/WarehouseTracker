using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTracker.Domain
{
    public class Event
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int ColleagueId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string EventType { get; set; } = null!;
        public int DepartmentId { get; set; } 
        public string Source { get; set; } = null!;
        public int ShiftAssignmentId { get; set; }
    }
}
