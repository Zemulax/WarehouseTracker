namespace WarehouseTracker.Api.Models
{
    /// <summary>
    /// Represents the shape of an incoming request to register a new department
    /// </summary>
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string DeparmentCode { get; set; } = string.Empty;
        public string DepartmentGroupCode { get; set; } = string.Empty;
    }
}
