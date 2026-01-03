using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTracker.Application
{
    /// <summary>
    /// Defines operations for registering a new colleague in the system.
    /// </summary>
    /// <remarks>Implementations of this interface provide methods to add colleague records, typically as part
    /// of an onboarding or personnel management workflow.</remarks>
    public interface IColleagueService
    {
        Task RegisterColleagueAsync(string employeeId, string firstName, string lastName);
    }
}
