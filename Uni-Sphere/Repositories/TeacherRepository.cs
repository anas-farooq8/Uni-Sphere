using Uni_Sphere.Data;
using Uni_Sphere.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Uni_Sphere.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private UniSphereDbContext _uniSphereDbContext;
        public TeacherRepository(UniSphereDbContext uniSphereDbContext)
        {
            _uniSphereDbContext = uniSphereDbContext;
        }

        public async Task<Teachers> AddAsync(Teachers teacher)
        {
            await _uniSphereDbContext.Teachers.AddAsync(teacher);
            await _uniSphereDbContext.SaveChangesAsync();

            return teacher;
        }

        public async Task<int> Count()
        {
            return await _uniSphereDbContext.Teachers.CountAsync();
        }

        public async Task<Teachers?> DeleteAsync(int id)
        {
            var teacher = await _uniSphereDbContext.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _uniSphereDbContext.Teachers.Remove(teacher);
                await _uniSphereDbContext.SaveChangesAsync();
                return teacher;
            }
            return null;
        }

        public async Task<IEnumerable<Teachers>> GetAllAsync()
        {
            return await _uniSphereDbContext.Teachers.ToListAsync();
        }

        public async Task<Teachers?> GetAsync(int id)
        {
            return await _uniSphereDbContext.Teachers.FindAsync(id);
        }

        public async Task<Teachers?> UpdateAsync(Teachers teacher)
        {
            var existingTeacher = await _uniSphereDbContext.Teachers.FindAsync(teacher.Id);
            if (existingTeacher != null)
            {
                existingTeacher.FullName = teacher.FullName;
                existingTeacher.Gender = teacher.Gender;
                existingTeacher.PhoneNo = teacher.PhoneNo;
                existingTeacher.DateOfBirth = teacher.DateOfBirth;
                existingTeacher.Designation = teacher.Designation;
                existingTeacher.Salary = teacher.Salary;
                existingTeacher.DepartmentId = teacher.DepartmentId;

                await _uniSphereDbContext.SaveChangesAsync();
                return existingTeacher;
            }
            return null;
        }
    }
}
