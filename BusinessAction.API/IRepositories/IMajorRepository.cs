using BusinessAction.API.Models.DBModels;

namespace BusinessAction.API.IRepositories
{
    public interface IMajorRepository
    {
        Task<Major> GetMajorByCourseNameAsync(string MajorName);
        Task<bool> AddStudentToMajorAsync(Major MajorName);
        Task<bool> RemoveStudentFromMajorAsync(Major MajorName);
    }
}
