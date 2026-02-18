using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;
using WarehouseTracker.Domain.Enums;

namespace WarehouseTracker.Application.Services
{
    public interface IBreakRuleService
    {
        Task AddAsync(BreakRule breakRule);
        Task<BreakRule?> GetBreakRule();
        Task<BreakRule?> GetBreakByType(BreakTypes breakType);


    }
}
