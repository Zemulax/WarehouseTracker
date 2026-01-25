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

        public async Task<BreakRule?> GetBreakRuleAsync()
        {
            // Use async database call and return the result
            return await _dbContext.BreakRules.FirstOrDefaultAsync();
        }

        public async Task<BreakRule?> GetBreakRuleByTypeAsync(string breakType)
        {
            return await _dbContext.BreakRules.FirstOrDefaultAsync(b => b.BreakType == breakType);
        }

        public async Task<bool> SetBreakRule(string BreakType, TimeOnly breakstart, TimeOnly breakend)
        {
            var existingbreakrule = await GetBreakRuleByTypeAsync(BreakType);
            Console.WriteLine($"here: {existingbreakrule}");
            if(existingbreakrule == null)
            {
                var breakRuleEntity = new BreakRule
                {
                    BreakType = BreakType,
                    BreakStart = breakstart,
                    BreakEnd = breakend
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
