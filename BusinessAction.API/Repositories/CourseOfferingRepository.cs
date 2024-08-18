using BusinessAction.API.Context;
using BusinessAction.API.IRepositories;
using BusinessAction.API.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace BusinessAction.API.Repositories
{
    public class CourseOfferingRepository : ICourseOfferingRepository
    {
        private readonly UniversityContext _context;

        public CourseOfferingRepository(UniversityContext context)
        {
            _context = context;
        }

        public async Task<CourseOffering> GetCourseOfferingsByCourseNameAsync(string courseName)
        {
            return await _context.CourseOfferings
                .Include(co => co.Course)
                .Include(co => co.Semester)
                .Where(co => co.Course.CourseName == courseName)
                .FirstOrDefaultAsync();
        }

        public async Task<CourseOffering> GetCourseOfferingByIdAsync(int id)
        {
            return await _context.CourseOfferings
                .Include(co => co.Course)
                .Include(co => co.Semester)
                .FirstOrDefaultAsync(co => co.CourseOfferingID == id);
        }

        public async Task UpdateCourseOfferingAsync(CourseOffering courseOffering)
        {
            _context.CourseOfferings.Update(courseOffering);
            await _context.SaveChangesAsync();
        }

    }
}
