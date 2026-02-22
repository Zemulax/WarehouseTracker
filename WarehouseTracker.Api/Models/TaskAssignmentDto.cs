using WarehouseTracker.Domain;

namespace WarehouseTracker.Api.Models
{
    public class TaskAssignmentDto
    {
        
        public string ColleagueId { get; set; } = null!;
        public string DepartmentCode { get; set; } = null!; // pick, pack, etc
        
    } 
}
