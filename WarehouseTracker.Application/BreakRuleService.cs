using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using WarehouseTracker.Domain;
using WarehouseTracker.Infrastructure;

namespace WarehouseTracker.Application
{
    public class BreakRuleService : IBreakRuleService
    {

        private readonly WarehouseTrackerDbContext _dbContext;

        //start from here. we should only be able to add break rule once.

        public BreakRuleService(WarehouseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BreakRule?> GetBreakRuleByTypeAsync(string breakType)
        {
            return await _dbContext.BreakRules.FirstOrDefaultAsync(b => b.BreakType == breakType);
        }

        public async Task<List<BreakRule>> GetBreakRulesAsync()
        {
            return await _dbContext.BreakRules.ToListAsync();
        }

        public async Task<bool> SetBreakRule(string BreakType, int shiftAfterMinutes, int durationMinutes)
        {
            var existingbreakrule = await GetBreakRuleByTypeAsync(BreakType);
            if(existingbreakrule == null)
            {
                var breakRuleEntity = new BreakRule
                {
                    BreakType = BreakType,
                    StartAfterMinutes = shiftAfterMinutes,
                    DurationMinutes = durationMinutes
                };

                _dbContext.BreakRules.Add(breakRuleEntity);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            else
            {
                return false;
            }
            
                
            
        }

        public async Task UpdateBreakRule(BreakRule newBreakRule)
        {
           
            _dbContext.BreakRules.Update(newBreakRule);
            await _dbContext.SaveChangesAsync();
        }


    }
}
