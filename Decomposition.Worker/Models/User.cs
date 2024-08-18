using System.ComponentModel.DataAnnotations;

namespace Decomposition.Worker.Models
{
    public class User
    {
        public string name { get; set; }
        public int age { get; set; }
        public int oib {  get; set; }
        public string dateOfBirth { get; set; }
        public string email { get; set; }
    }
}
