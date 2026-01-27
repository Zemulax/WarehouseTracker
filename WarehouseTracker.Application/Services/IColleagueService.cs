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
        Task RegisterColleagueAsync(
            int Id,
            string employeeId, 
            string firstName, 
            string lastName,
            string role,
            bool isActive );

        Task<List<Colleague>> RetrieveColleagueAsync();

        Task<List<Colleague>> RetrieveColleagueByAttribute(

            int? Id,
            string? employeeId,
            string? firstName,
            string? lastName,
            string? role,
            bool? isActive
            );

        Task DeleteColleagueAsync(string employeeId);
        Task UpdateColleagueAsync(
            string employeeId,
            string? firstName,
            string? lastName,
            string? role,
            bool? isActive);
    }


}
