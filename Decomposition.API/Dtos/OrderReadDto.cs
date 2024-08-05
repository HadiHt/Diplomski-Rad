using Decomposition.API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Decomposition.API.Dtos
{
    public class OrderReadDto
    {
        public int OrderId { get; set; }
        public string OrderState { get; set; }
        public string OrderType { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.Now;
        public User User { get; set; }
    }
}
