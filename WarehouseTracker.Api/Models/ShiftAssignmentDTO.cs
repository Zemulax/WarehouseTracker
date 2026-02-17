namespace WarehouseTracker.Api.Models
{
    public class ShiftAssignmentDTO
    {
        public string ColleagueId { get; set; } = null!;
        public DateTimeOffset ShiftStart { get; set; }
        public DateTimeOffset ShiftEnd { get; set; }
    } 
}
