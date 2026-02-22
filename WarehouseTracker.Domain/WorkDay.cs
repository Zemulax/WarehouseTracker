using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTracker.Domain;

public class WorkDay
{
    public int Id { get; set; }
    public string ColleagueId { get; set; } = string.Empty;
    public Colleague Colleague { get; set; } = null!;
    public DateTimeOffset WorkDayStart { get; set; }
    public string Status { get; set; } = string.Empty; //Active, Break, SignedOut
    public DateTimeOffset? WorkDayEnd { get; set; }
    public ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();

}