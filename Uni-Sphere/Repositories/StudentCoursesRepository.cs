using Uni_Sphere.DataAccess.Data;
using Uni_Sphere.Models.DTO;
using Uni_Sphere.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uni_Sphere.Repositories
{
    public class StudentCoursesRepository : IStudentCoursesRepository
    {
        private readonly UniSphereDbContext _uniSphereDbContext;

        public StudentCoursesRepository(UniSphereDbContext uniSphereDbContext)
        {
            // Ensure the context is properly injected
            _uniSphereDbContext = uniSphereDbContext ?? throw new ArgumentNullException(nameof(uniSphereDbContext));
        }

        public async Task<IEnumerable<CourseDisplayDTO>> GetCoursesByStudentAsync(int studentId)
        {
            // Validate studentId
            if (studentId <= 0)
            {
                throw new ArgumentException("Invalid student ID", nameof(studentId));
            }

            try
            {
                // Ensure courses are being loaded correctly
                var courses = await _uniSphereDbContext.Courses
                    .Where(c => c.Students.Any(s => s.Id == studentId))
                    .Select(course => new CourseDisplayDTO
                    {
                        Id = course.Id,
                        Name = course.Name,
                        Code = course.Code,
                        CreditHours = course.CreditHours,
                        CourseType = course.CourseType.ToString(),
                        IsLab = course.IsLab,
                        Description = course.Description,
                    })
                    .ToListAsync();

                // Return courses or empty list to avoid null references
                return courses ?? new List<CourseDisplayDTO>();
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
                throw new Exception("Error occurred while fetching courses for the student.", ex);
            }
        }

        public async Task<bool> RegisterCoursesAsync(int studentId, List<int> courseIds)
        {
            var student = await _uniSphereDbContext.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null) return false;

            foreach (var courseId in courseIds)
            {
                var course = await _uniSphereDbContext.Courses.FindAsync(courseId);
                if (course != null && !student.Courses.Contains(course))
                {
                    student.Courses.Add(course);
                }
            }

            await _uniSphereDbContext.SaveChangesAsync();
            return true;
        }
    }
}
