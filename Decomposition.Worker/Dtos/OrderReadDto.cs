using Decomposition.Worker.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Decomposition.Worker.Dtos
{
    public class OrderReadDto
    {
        public int OrderId { get; set; }

        public Guid newGuid { get; set; } = Guid.NewGuid();

        public string OrderState { get; set; }
        public string OrderType { get; set; }

        public DateTime TimeCreated { get; set; } = DateTime.Now;

        public User UserObject { get; set; }
    }
}
