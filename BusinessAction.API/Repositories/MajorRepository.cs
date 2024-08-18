using BusinessAction.API.Context;
using BusinessAction.API.IRepositories;
using BusinessAction.API.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace BusinessAction.API.Repositories
{
    public class MajorRepository : IMajorRepository
    {
        private readonly UniversityContext _context;

        public MajorRepository(UniversityContext context) {
            _context = context;
        }

        public async Task<bool> AddStudentToMajorAsync(Major Major)
        {
            _context.Majors.Update(Major);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Major> GetMajorByCourseNameAsync(string majorName)
        {
            return await _context.Majors.Where(x => x.MajorName == majorName).FirstOrDefaultAsync();
        }

        public async Task<bool> RemoveStudentFromMajorAsync(Major Major)
        {
            _context.Majors.Update(Major);
            var x = await _context.SaveChangesAsync();
                return true;

        }
    }
}
