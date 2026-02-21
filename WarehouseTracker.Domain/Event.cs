using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Api.Enums;

namespace WarehouseTracker.Domain
{
    public class Event
    {
        public int Id { get; set; }

        public string ColleagueId { get; set; } = null!;

        public string? DepartmentCode { get; set; }

        public int TaskAssignmentId { get; set; }

        public EventTypes EventType { get; set; }

        public DateTimeOffset TimestampUtc { get; set; }

        public string Source { get; set; } = "User";
    }
}
