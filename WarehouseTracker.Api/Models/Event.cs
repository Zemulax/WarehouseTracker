using WarehouseTracker.Api.Enums;

namespace WarehouseTracker.Api.Models
{
    public class Event
    {

        public int ColleagueId { get; set; }

        public int? DepartmentId { get; set; }

        public int ShiftAssignmentId { get; set; }

        public string EventType { get; set; } = null!;

        public DateTime TimestampUtc { get; set; }

        public string Source { get; set; } = "User";
    }
}
