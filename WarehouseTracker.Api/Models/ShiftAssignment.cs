namespace WarehouseTracker.Api.Models
{
    public class ShiftAssignment
    {
        public int Id { get; set; }
        public int ColleagueId { get; set; }
        public int DepartmentId { get; set; }
        public DateOnly ShiftDate { get; set; }
        public TimeOnly ShiftStart { get; set; }
        public TimeOnly ShiftEnd { get; set; }
    }
}
