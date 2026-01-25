using WarehouseTracker.Domain;

namespace WarehouseTracker.Application
{
    public interface IDepartmentService
    {
        Task RegisterDepartmentAsync(
            int Id,
            string departmentName,
            string deparmentCode,
            string departmentGroupCode);
        Task<List<Department>> RetrieveDepartmentAsync();
        Task<List<Department>> RetrieveDepartmentByAttribute(
            int? Id,
            string? departmentName,
            string? deparmentCode,
            string? departmentGroupCode
            );
        Task DeleteDepartmentAsync(string deparmentCode);
        Task UpdateDepartmentAsync(
            string deparmentCode,
            string? departmentName,
            string? departmentGroupCode);
    }
}
