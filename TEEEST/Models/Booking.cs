using System.Text.Json.Serialization;

namespace TEEEST.Models
{
    public class Booking
    {
        public int Id { get; set; } // Removed [JsonIgnore]

        public required string BookingType { get; set; }
        public required decimal Price { get; set; }
        public required TimeSpan Duration { get; set; }

        [JsonIgnore]
        public DateTime StartTimeUtc { get; set; } = DateTime.UtcNow;

        // Georgian time properties
        public string Date => ToGeorgianTime(StartTimeUtc).ToString("dd.MM.yyyy");
        public string Start => ToGeorgianTime(StartTimeUtc).ToString("HH:mm");
        public string End => ToGeorgianTime(StartTimeUtc.Add(Duration)).ToString("HH:mm");
        public string DurationDisplay => $"{Duration.Hours}h {Duration.Minutes}m";
        public string TimeSummary => $"{BookingType} at {Start}-{End} ({DurationDisplay})";

        // Timer properties
        public string TimeRemaining
        {
            get
            {
                var endTime = StartTimeUtc.Add(Duration);
                var remaining = endTime - DateTime.UtcNow;
                return remaining > TimeSpan.Zero
                    ? $"{remaining.Hours}h {remaining.Minutes}m {remaining.Seconds}s"
                    : "Ended";
            }
        }

        public bool IsEnded => DateTime.UtcNow > StartTimeUtc.Add(Duration);

        public void UpdateEndedStatus()
        {
            // Example logic: can be extended to update a database
            if (IsEnded)
            {
                Console.WriteLine($"Booking {Id} has ended.");
            }
        }

        public static DateTime ToGeorgianTime(DateTime dateTime)
        {
            try
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById(
                    OperatingSystem.IsWindows()
                        ? "Georgian Standard Time"
                        : "Asia/Tbilisi");
                return TimeZoneInfo.ConvertTimeFromUtc(dateTime, tz);
            }
            catch
            {
                return dateTime.AddHours(4); // Fallback to UTC+4
            }
        }
    }
}