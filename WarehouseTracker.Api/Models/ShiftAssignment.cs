namespace WarehouseTracker.Api.Models
{
    public class ShiftAssignment
    {
        public string ColleagueId { get; set; } = null!;
        public int DepartmentId { get; set; }
        public DateOnly ShiftDate { get; set; }
        public TimeOnly ShiftStart { get; set; }
        public TimeOnly ShiftEnd { get; set; }
    }
}
