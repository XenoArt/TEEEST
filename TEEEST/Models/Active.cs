using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TEEEST.Models
{
    public class Active
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public int SecInTimer { get; set; }

        public bool Mimdinare { get; set; }

        [Required]  // optional, remove if Type can be empty
        [StringLength(50, ErrorMessage = "Type cannot exceed 50 characters.")]
        public string Type { get; set; }
    }
}
