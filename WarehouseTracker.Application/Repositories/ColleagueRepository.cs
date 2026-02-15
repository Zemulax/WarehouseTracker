using Microsoft.EntityFrameworkCore;
using WarehouseTracker.Application.Services;
using WarehouseTracker.Domain;
using WarehouseTracker.Infrastructure;

namespace WarehouseTracker.Application.Repositories
{

    public class ColleagueRepository : IColleagueRepository
    {
        private readonly WarehouseTrackerDbContext _dbContext;

        public ColleagueRepository(WarehouseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Colleague colleague)
        {
           await _dbContext.Colleagues.AddAsync(colleague);
        }

        public async Task<List<Colleague>> GetAll()
        {
            return await _dbContext.Colleagues.ToListAsync();
               
        }

        public async Task<Colleague?> GetByIdAsync(string ColleagueId)
        {
            return await _dbContext.Colleagues.SingleOrDefaultAsync
                (c => c.ColleagueId == ColleagueId);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
