using WarehouseTracker.Api.Enums;

namespace WarehouseTracker.Api.Models
{
    public class EventDto
    {

        public string ColleagueId { get; set; } = null!;
        public string? DepartmentCode { get; set; }
        public EventTypes EventType { get; set; }
        public DateTimeOffset TimestampUtc { get; set; }
    }
}
