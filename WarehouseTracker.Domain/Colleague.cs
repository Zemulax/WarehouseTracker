namespace WarehouseTracker.Domain
{
    /// <summary>
    /// This is the Colleague entity representing a colleague in the warehouse tracking system.
    /// </summary>
    public class Colleague
    {
        public int Id { get; set; }
        public string ColleagueId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;


    }
}
