using BusinessAction.API.Context;
using BusinessAction.API.IRepositories;
using BusinessAction.API.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace BusinessAction.API.Repositories
{
    public class EnrollementRepo : IEnrollementRepo
    {

        private readonly UniversityContext _context;

        public EnrollementRepo(UniversityContext context)
        {
            _context = context;
        }
        public async Task<bool> EnrollStudentInCourseAsync(Student student, int courseOfferingId)
        {
            var courseOffering = await _context.CourseOfferings
            .FirstOrDefaultAsync(co => co.CourseOfferingID == courseOfferingId);

            if (courseOffering == null || courseOffering.SeatsAvailable <= 0)
                return false;

            var enrollment = new Enrollment
            {
                StudentID = student.StudentID,
                CourseOfferingID = courseOffering.CourseOfferingID,
                EnrollmentDate = DateTime.Now,
                Student = student,
                CourseOffering = courseOffering
            };

            _context.Enrollments.Add(enrollment);

            courseOffering.SeatsAvailable--;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Enrollment>> GetEnrollmentAsync(int studentId)
        {
            var enrollments = await _context.Enrollments
                .Where(e => e.StudentID == studentId)
                .ToListAsync();

            return enrollments;
        }
    }
}
