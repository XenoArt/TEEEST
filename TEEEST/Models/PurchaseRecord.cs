using System.Text.Json.Serialization;

namespace TEEEST.Models
{
    public class PurchaseRecord
    {
        [JsonIgnore]
        public int Id { get; set; }  // This will be auto-incremented by the database

        public DateTime Date { get; set; }
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Price { get; set; }
        public int ItemsPurchased { get; set; }
    }
}
