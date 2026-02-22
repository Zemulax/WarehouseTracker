namespace WarehouseTracker.Api.Models
{
    public class ActivitySessionDto
    {
        public string ColleagueId { get; set; } = null!;
        public string? DepartmentCode { get; set; } = null!; // pick, pack, etc
        public int WorkDayId{ get; set; }
        public string SessionType { get; set; } = null!;
        public DateTimeOffset SessionStart { get; set; }
        public DateTimeOffset SessionEnd { get; set; }
    }
}
