using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTracker.Application.Services
{
    public interface IEventService
    {
        //public Guid Id { get; set; } = Guid.NewGuid();
       // public int ColleagueId { get; set; }
        //public DateTime Timestamp { get; set; } = DateTime.Now;
       // public EventTypes EventType { get; set; } = null!;
       // public string DepartmentId { get; set; } = null!;
      //  public string Source { get; set; } = null!;
        Task LogEventAsync(
            string eventType,
            int colleagueId,
            int departmentId,
            DateTime eventTimestamp,
            string source,
            int ShiftAssignmentId
            );

        Task<List<Domain.Event>> RetrieveEventsAsync();

        Task<List<Domain.Event>> RetrieveEventsByAttribute(
            int? colleagueId,
            DateTime? eventTimestamp,
            string? eventType,
            int? departmentId,
            string? source,
            int? ShiftAssignmentId);


    }
}
