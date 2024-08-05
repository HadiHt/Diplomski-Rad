using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Decomposition.Worker.Models
{
    public class Order
    {
        [Key]
        [Required]
        public int OrderId { get; set; }

        [Required]
        public string OrderState { get; set; }
        [Required]
        public string OrderType { get; set; }

        [Required]
        public DateTime TimeCreated { get; set; } = DateTime.Now;

        [Required]
        public string UserObject { get; set; }
    }
}
