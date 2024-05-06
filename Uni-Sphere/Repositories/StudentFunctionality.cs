using Microsoft.EntityFrameworkCore;
using Uni_Sphere.DataAccess.Data;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Repositories.IRepositories;

namespace Uni_Sphere.Repositories
{
    public class StudentFunctionality(UniSphereDbContext uniSphereDbContext) : IStudentFunctionality
    {
        private UniSphereDbContext _uniSphereDbContext = uniSphereDbContext;

        public async Task<IEnumerable<Attendance>> GetAttendanceOfCourse(int studentId, int courseId)
        {
            return await _uniSphereDbContext.Attendance
                .Where(a => a.StudentsId == studentId && a.CoursesId == courseId)
                .ToListAsync();
        }


        public async Task<int> GetStudentIdByUserEmailAsync(string email)
        {
            var student = await _uniSphereDbContext.Students
                .Where(s => s.Email == email)
                .FirstOrDefaultAsync();

            return student.Id;
        }
    }
}
