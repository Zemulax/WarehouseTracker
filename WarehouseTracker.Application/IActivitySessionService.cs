using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application
{
    public interface IActivitySessionService
    {
        Task StartSessionAsync(
            int Id,
            int ColleagueId,
            int DepartmentId,
            int ShiftAssignmentId,
            string SessionType,
            TimeOnly sessionStart,
            TimeOnly sessionEnd
            

            );

        
        Task<List<ActivitySession>> RetrieveActivitySessionsAsync();

        Task<List<ActivitySession>> RetrieveActivitySessionsByAttribute(
            int? Id,
           int? ColleagueId,
            int? DepartmentId,
            int? ShiftAssignmentId,
            string? SessionType,
            TimeOnly? sessionStart,
            TimeOnly? sessionEnd
            

            );


    }
}
