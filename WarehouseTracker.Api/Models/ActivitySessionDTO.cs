namespace WarehouseTracker.Api.Models
{
    public class ActivitySessionDTO
    {
        public string ColleagueId { get; set; } = null!;
        public int? DepartmentId { get; set; }
        public int ShiftAssignmentId { get; set; }
        public string SessionType { get; set; } = null!;
        public DateTimeOffset SessionStart { get; set; }
        public DateTimeOffset SessionEnd { get; set; }
    }
}
