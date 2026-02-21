namespace WarehouseTracker.Api.Models
{
    /// <summary>
    /// Represents the shape of an incoming request to register a new department
    /// </summary>
    public class DepartmentDto
    {
        public string DepartmentName { get; set; } = string.Empty;
        public string DepartmentCode { get; set; } = string.Empty;
        public string DepartmentGroupCode { get; set; } = string.Empty;
    }   
}
