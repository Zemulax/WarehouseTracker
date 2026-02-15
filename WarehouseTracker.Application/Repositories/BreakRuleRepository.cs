using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using WarehouseTracker.Application.Services;
using WarehouseTracker.Domain;
using WarehouseTracker.Infrastructure;

namespace WarehouseTracker.Application.Repositories
{
    public class BreakRuleRepository : IBreakRuleRepository
    {
        

        private readonly WarehouseTrackerDbContext _dbContext;

        public BreakRuleRepository(WarehouseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(BreakRule breakRule)
        {
            await _dbContext.BreakRules.AddAsync(breakRule);
        }

        public async Task<BreakRule> GetBreakRule()
        {
            return await _dbContext.BreakRules.FirstAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
