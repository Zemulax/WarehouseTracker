namespace WarehouseTracker.Api.Models
{
    public class BreakRule
    {
        public int Id { get; set; }
        public string BreakType { get; set; } = null!;
        public TimeOnly BreakStart { get; set; }
        public TimeOnly BreakEnd { get; set; }
    }
}
