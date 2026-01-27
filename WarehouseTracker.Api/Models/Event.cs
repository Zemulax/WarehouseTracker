using WarehouseTracker.Api.Enums;

namespace WarehouseTracker.Api.Models
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
