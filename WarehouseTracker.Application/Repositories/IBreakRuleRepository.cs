using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;
using WarehouseTracker.Domain.Enums;

namespace WarehouseTracker.Application.Repositories
{
    public interface IBreakRuleRepository
    {
        Task AddAsync(BreakRule breakRule);
        Task<BreakRule> GetBreakRule();
        Task<BreakRule?> GetBreakRuleByType(BreakTypes breakType);

        Task SaveChangesAsync();
    }
}
