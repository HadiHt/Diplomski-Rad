using Decomposition.Worker.Models;

namespace Decomposition.Worker.Dtos
{
    public class OrderWriteDto
    {
        public string Id { get; set; }
        public string OrderState { get; set; }
        public string OrderType { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.Now;
        public User UserData { get; set; }
    }
}
