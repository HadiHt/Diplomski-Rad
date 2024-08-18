using BusinessAction.API.Models.DBModels;

namespace BusinessAction.API.IRepositories
{
    public interface ICourseOfferingRepository
    {
        Task<CourseOffering> GetCourseOfferingsByCourseNameAsync(string courseName);
        Task<CourseOffering> GetCourseOfferingByIdAsync(int id);
        Task UpdateCourseOfferingAsync(CourseOffering courseOffering);
    }
}
