using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Repositories
{
    public interface IDepartmenRepository
    {
        Task AddAsync(Department department);
        Task<Department> GetByCodeAsync(string code);
        Task<List<Department>> GetAllAsync();
        Task SaveChangeAsync();

    }
}
