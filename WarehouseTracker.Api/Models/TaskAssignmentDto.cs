namespace WarehouseTracker.Api.Models
{
    public class TaskAssignmentDto
    {
        public string ColleagueId { get; set; } = null!;
        public DateTimeOffset ShiftStart { get; set; }
        public DateTimeOffset ShiftEnd { get; set; }
    } 
}
