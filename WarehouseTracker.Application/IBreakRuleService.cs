using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application
{
    public interface IBreakRuleService
    {
        Task<bool> SetBreakRule(string BreakType, TimeOnly breakstart, TimeOnly breakEnd);

        Task UpdateBreakRule(BreakRule newBreakRule);

        Task<List<BreakRule>> GetBreakRulesAsync(); 

        Task<BreakRule?> GetBreakRuleByTypeAsync(string breakType);


    }
}
