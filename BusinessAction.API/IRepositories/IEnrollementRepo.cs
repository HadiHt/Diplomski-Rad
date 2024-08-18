using BusinessAction.API.Models.DBModels;

namespace BusinessAction.API.IRepositories
{
    public interface IEnrollementRepo
    {
        Task<bool> EnrollStudentInCourseAsync(Student studentId, int courseOfferingId);
        Task<List<Enrollment>> GetEnrollmentAsync(int studentId);
    }
}
