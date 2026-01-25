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

        Task<BreakRule?> GetBreakRuleAsync();

        Task<BreakRule?> GetBreakRuleByTypeAsync(string breakType);


    }
}
