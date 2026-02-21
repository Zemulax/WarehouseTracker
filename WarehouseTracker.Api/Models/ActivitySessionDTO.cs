namespace WarehouseTracker.Api.Models
{
    public class ActivitySessionDto
    {
        public string ColleagueId { get; set; } = null!;
        public int? DepartmentId { get; set; }
        public int TaskAssignmentId { get; set; }
        public string SessionType { get; set; } = null!;
        public DateTimeOffset SessionStart { get; set; }
        public DateTimeOffset SessionEnd { get; set; }
    }
}
