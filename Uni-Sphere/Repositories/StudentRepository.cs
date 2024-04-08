using Uni_Sphere.Data;
using Uni_Sphere.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Uni_Sphere.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private UniSphereDbContext _uniSphereDbContext;
        public StudentRepository(UniSphereDbContext uniSphereDbContext)
        {
            _uniSphereDbContext = uniSphereDbContext;
        }

        public async Task<Students> AddAsync(Students student)
        {
            await _uniSphereDbContext.Students.AddAsync(student);
            await _uniSphereDbContext.SaveChangesAsync();

            return student;
        }

        public async Task<int> Count()
        {
            return await _uniSphereDbContext.Students.CountAsync();
        }

        public async Task<Students?> DeleteAsync(int id)
        {
            var student = await _uniSphereDbContext.Students.FindAsync(id);
            if (student != null)
            {
                _uniSphereDbContext.Students.Remove(student);
                await _uniSphereDbContext.SaveChangesAsync();
                return student;
            }
            return null;
        }

        public async Task<IEnumerable<Students>> GetAllAsync()
        {
            return await _uniSphereDbContext.Students.ToListAsync();
        }

        public async Task<Students?> GetAsync(int id)
        {
            return await _uniSphereDbContext.Students.FindAsync(id);
        }

        public async Task<Students?> UpdateAsync(Students student)
        {
            var existingStudent = await _uniSphereDbContext.Students.FindAsync(student.Id);
            if (existingStudent != null)
            {
                existingStudent.FullName = student.FullName;
                existingStudent.Gender = student.Gender;
                existingStudent.PhoneNo = student.PhoneNo;
                existingStudent.Section = student.Section;
                existingStudent.Degree = student.Degree;
                existingStudent.CurrentSemester = student.CurrentSemester;
                existingStudent.Gpa = student.Gpa;
                existingStudent.Credits = student.Credits;
                existingStudent.DepartmentId = student.DepartmentId;

                await _uniSphereDbContext.SaveChangesAsync();
                return existingStudent;
            }
            return null;
        }
    }
}
