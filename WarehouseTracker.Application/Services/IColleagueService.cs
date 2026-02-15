using WarehouseTracker.Domain;
namespace WarehouseTracker.Application.Services
{
    /// <summary>
    /// Defines operations for registering a new colleague in the system.
    /// </summary>
    /// <remarks>Implementations of this interface provide methods to add colleague records, typically as part
    /// of an onboarding or personnel management workflow.</remarks>
    public interface IColleagueService
    {
        Task RegisterColleagueAsync(Colleague colleague);
        Task<List<Colleague>> RetrieveColleagueAsync();
        Task<Colleague> GetColleagueById(string colleagueId);
    }
}
