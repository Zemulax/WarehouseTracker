using Microsoft.EntityFrameworkCore;

namespace WarehouseTracker.Api.Models
{
    /// <summary>
    /// Represents the shape of an incoming request to register a new colleague
    /// </summary>
    public class Colleague
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        }
}
