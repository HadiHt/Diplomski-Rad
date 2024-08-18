namespace BusinessAction.API.Models.DBModels
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public int CourseOfferingID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public double GPA { get; set; }
        public Student Student { get; set; }
        public CourseOffering CourseOffering { get; set; }
    }
}
