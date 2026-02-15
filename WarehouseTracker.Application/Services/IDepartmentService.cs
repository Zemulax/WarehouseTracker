using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Services
{

    public interface IDepartmentService
    {
        Task CreateAsync(Department request);
        Task<Department> GetByCodeAsync(string code);
        Task<List<Department>> GetAllAsync();
    }
}
