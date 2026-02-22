using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Services
{
    public interface IWorkDayService
    {
        Task<WorkDay?> GetActiveWorkDay(string colleagueId);
        Task StartWorkDayAsync(string colleagueId);
        Task EndWorkDayAsync(string colleagueId);

        Task<List<WorkDay>> GetAllActiveWorkDaysAsync();
    }
}
