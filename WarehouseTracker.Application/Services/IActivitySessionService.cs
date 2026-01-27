using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Services
{
    public interface IActivitySessionService
    {
        Task StartSessionAsync(
            
            int ColleagueId,
            int DepartmentId,
            int ShiftAssignmentId,
            string SessionType,
            TimeOnly sessionStart

            );

        
        Task<List<ActivitySession>> RetrieveActivitySessionsAsync();

        Task<List<ActivitySession>> RetrieveActivitySessionsByAttribute(
            
           int? ColleagueId,
            int? DepartmentId,
            int? ShiftAssignmentId,
            string? SessionType,
            TimeOnly? sessionStart,
            TimeOnly? sessionEnd
            

            );

        Task ReplaceForShiftAsync(
            int ShiftAssignmentId,
            IReadOnlyList<ActivitySession> sessions
            );


    }
}
