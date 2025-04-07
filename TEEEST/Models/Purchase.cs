using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TEEEST.Models
{
    public class Purchase
    {
        [JsonIgnore] // Will be ignored in input
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [RegularExpression(@"^[0-5]?\d:[0-5]\d$", ErrorMessage = "Duration must be in mm:ss format")]
        public string Duration { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public string Products { get; set; }
        [JsonIgnore] // Will be ignored in input
        public string FormattedDate { get; set; }

        [JsonIgnore] // Will be ignored in input
        public string FormattedTime { get; set; }
    }
}
