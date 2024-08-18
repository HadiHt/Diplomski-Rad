using BusinessAction.API.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace BusinessAction.API.Context
{
    public class UniversityContext : DbContext
    {

        public DbSet<Major> Majors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<CourseOffering> CourseOfferings { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Major)
                .WithMany()
                .HasForeignKey(s => s.MajorID);

            modelBuilder.Entity<CourseOffering>()
                .HasOne(co => co.Course)
                .WithMany()
                .HasForeignKey(co => co.CourseID);

            modelBuilder.Entity<CourseOffering>()
                .HasOne(co => co.Semester)
                .WithMany()
                .HasForeignKey(co => co.SemesterID);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.CourseOffering)
                .WithMany()
                .HasForeignKey(e => e.CourseOfferingID);
        }

    }
}
