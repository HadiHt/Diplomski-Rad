namespace BusinessAction.API.Models.DBModels
{
    public class Major
    {
        public int MajorID { get; set; }
        public string MajorName { get; set; }
        public string Department { get; set; }
        public int AvailableSeats { get; set; }
        public int MaxSeats { get; set; }
    }
}
