using BusinessAction.API.Models.DBModels;

namespace BusinessAction.API.IRepositories
{
    public interface IStudentRepository
    {
        Task<bool> AddStudentAsync(Student student);
        Task<Student> GetStudentByEmailAsync(string email);
    }
}
