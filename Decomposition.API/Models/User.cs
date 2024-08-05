using System.ComponentModel.DataAnnotations;

namespace Decomposition.API.Models
{
    public class User
    {
        [Key]
        [Required]
        public int UserId { get; set; }

        [Required]
        public string name { get; set; }
        [Required]
        public int age { get; set; }
    }
}
