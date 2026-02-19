using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using WarehouseTracker.Application.Services;
using WarehouseTracker.Domain;
using WarehouseTracker.Domain.Enums;
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

        public async Task<List<BreakRule>> GetAllBreakRules()
        {
            return await _dbContext.BreakRules.ToListAsync();
        }

        public async Task<BreakRule> GetBreakRule()
        {
            return await _dbContext.BreakRules.FirstAsync();
        }

        public async Task<BreakRule?> GetBreakRuleByType(BreakTypes breakType)
        {
            return await _dbContext.BreakRules
                .SingleOrDefaultAsync(bt => bt.BreakType == breakType);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(BreakRule newBreakRule)
        {
            var existing = await _dbContext.BreakRules
         .FirstOrDefaultAsync(x => x.BreakType == newBreakRule.BreakType);

            if (existing == null)
                throw new Exception("Break rule not found");

            existing.BreakStart = newBreakRule.BreakStart;
            existing.BreakEnd = newBreakRule.BreakEnd;

            _dbContext.BreakRules.Add(existing);
            await _dbContext.SaveChangesAsync();

        }
    }
}
