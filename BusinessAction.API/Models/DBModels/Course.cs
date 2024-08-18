namespace BusinessAction.API.Models.DBModels
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int MaxSeats { get; set; }
    }
}
