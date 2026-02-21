

namespace WarehouseTracker.Domain
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string DepartmentCode { get; set; } = string.Empty;
        public string DepartmentGroupCode { get; set; } = string.Empty;
    }
}
