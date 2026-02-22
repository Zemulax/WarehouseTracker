using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Repositories
{
    public interface IWorkDayRepository
    {
        Task AddWorkDayAsync(WorkDay workDay);
        Task<WorkDay?> GetActiveWorkDayForColleagueAsync(string colleagueId);
        Task<List<WorkDay>> GetAllWorkDaysForColleague(string colleagueId);
        Task<List<WorkDay>> GetAllActiveWorkDaysAsync();
        Task UpdateWorkDayAsync(WorkDay workDay);
        Task <WorkDay?>GetByIdAsync(int workDayId);
        Task SaveChangesAsync();
    }
}
