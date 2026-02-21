using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Repositories
{
    public interface IColleagueRepository
    {
        Task AddAsync(Colleague colleague);
        Task<Colleague?> GetByIdAsync(string colleagueId);
        Task<List<Colleague>> GetAll();
        Task SaveChangesAsync();
    }
}
