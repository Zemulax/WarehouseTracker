using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Application.Repositories;
using WarehouseTracker.Domain;
using WarehouseTracker.Domain.Enums;

namespace WarehouseTracker.Application.Services
{
    public class BreakRuleService : IBreakRuleService
    {
        private readonly IBreakRuleRepository _repository;
        public BreakRuleService(IBreakRuleRepository breakRuleRepository) {
         _repository = breakRuleRepository;
        }
        public async Task AddAsync(BreakRule breakRule)
        {
            await _repository.AddAsync(breakRule);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<BreakRule>> GetAllBreakRules()
        {
           return await _repository.GetAllBreakRules();
        }

        public async Task<BreakRule?> GetBreakByType(BreakTypes breakType)
        {
            return await _repository.GetBreakRuleByType(breakType);
        }

        public async Task<BreakRule?> GetBreakRule()
        {
            return await _repository.GetBreakRule();
        }

        public Task UpdateAsync(BreakRule newBreakRule)
        {
            _repository.UpdateAsync(newBreakRule);
            return Task.CompletedTask;
        }
    }
}
