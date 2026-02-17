using WarehouseTracker.Api.Enums;

namespace WarehouseTracker.Api.Models
{
    public class EventDTO
    {

        public string ColleagueId { get; set; } = null!;
        public string DepartmentCode { get; set; } = null!;
        public string EventType { get; set; } = null!;
        public DateTimeOffset TimestampUtc { get; set; }
    }
}
