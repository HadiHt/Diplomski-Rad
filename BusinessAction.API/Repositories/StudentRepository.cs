using BusinessAction.API.Context;
using BusinessAction.API.IRepositories;
using BusinessAction.API.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace BusinessAction.API.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UniversityContext _context;
        public StudentRepository(UniversityContext context) {
            _context = context;
        }
        public async Task<bool> AddStudentAsync(Student student)
        {
            var existingEntity = _context.ChangeTracker.Entries<Student>()
                             .FirstOrDefault(e => e.Entity == student);

            if (existingEntity != null)
            {
                existingEntity.State = EntityState.Detached;
            }
            await _context.Students.AddAsync(student);

            var entityState = _context.Entry(student).State;
            if (entityState != EntityState.Added)
            {
                throw new Exception("The entity state is not 'Added'.");
            }

            var result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                throw new Exception("No rows were affected by the save operation.");
            }
            return result > 0;
        }

        public async Task<Student> GetStudentByEmailAsync(string email)
        {
            return await _context.Students.Where(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
