namespace WarehouseTracker.Api.Models
{
    public class ActivitySession
    {
        public string ColleagueId { get; set; } = null!;
        public int DepartmentId { get; set; }
        public int ShiftAssignmentId { get; set; }
        public string SessionType { get; set; } = null!;
        public TimeOnly SessionStart { get; set; }
        public TimeOnly SessionEnd { get; set; }
    }
}
