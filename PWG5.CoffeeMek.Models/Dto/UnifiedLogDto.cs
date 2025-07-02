namespace PWG5.CoffeeMek.Data.Models
{
    public class UnifiedLogDto
    {
        public string Source { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }

        public string? Status { get; set; }

        public string? Location { get; set; }

        public Dictionary<string, string>? Details { get; set; } = new();
    }
}
