using Decomposition.API.Models;

namespace Decomposition.API.Dtos
{
    public class OrderWriteDto
    {
        public int OrderId { get; set; }
        public string name { get; set; }
        public string OrderType { get; set; }
        public User User { get; set; }
    }
}
