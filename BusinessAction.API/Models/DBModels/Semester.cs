namespace BusinessAction.API.Models.DBModels
{
    public class Semester
    {
        public int SemesterID { get; set; }
        public string SemesterName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
