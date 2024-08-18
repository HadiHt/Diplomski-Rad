namespace BusinessAction.API.Models.DBModels
{
    public class CourseOffering
    {
        public int CourseOfferingID { get; set; }
        public int CourseID { get; set; }
        public int SemesterID { get; set; }
        public int SeatsAvailable { get; set; }
        public Course Course { get; set; }
        public Semester Semester { get; set; }
    }
}
