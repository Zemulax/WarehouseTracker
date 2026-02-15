namespace WarehouseTracker.Api.Models
{
    public class BreakRule
    {
        public string BreakType { get; set; } = null!;
        public int StartAfterMinutes { get; set; }
        public int DurationMinutes { get; set; }
    }
}
