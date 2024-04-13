using Uni_Sphere.DataAccess.Data;
using Uni_Sphere.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Uni_Sphere.Repositories.IRepositories;
using Uni_Sphere.Models.DTO;

namespace Uni_Sphere.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private UniSphereDbContext _uniSphereDbContext;
        private UserManager<IdentityUser> _userManager;

        public StudentRepository(UniSphereDbContext uniSphereDbContext, UserManager<IdentityUser> userManager)
        {
            _uniSphereDbContext = uniSphereDbContext;
            _userManager = userManager;
        }

        public async Task<Students> AddAsync(Students student)
        {
            await _uniSphereDbContext.Students.AddAsync(student);
            await _uniSphereDbContext.SaveChangesAsync();

            return student;
        }

        public async Task<bool> CreateAccount(string username, string email, string password)
        {
            // Authorization & Authentication
            var identityUser = new IdentityUser
            {
                UserName = username,
                Email = email,
            };

            var identityResult = await _userManager.CreateAsync(identityUser, password);

            if (identityResult.Succeeded)
            {
                // Assigning Student Role
                var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, "Student");

                if (roleIdentityResult.Succeeded)
                {
                    // Success
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> DeleteAccount(string username)
        {
            var identityUser = await _userManager.FindByNameAsync(username);
            if (identityUser != null)
            {
                var result = await _userManager.DeleteAsync(identityUser);
                if (result.Succeeded && result is not null)
                {
                    return true;
                }
            }
            return false;
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

        public async Task<IEnumerable<StudentDTO>> GetAllAsync()
        {
            var studentsDTO = await _uniSphereDbContext.Students
                .Select(x => new StudentDTO
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    RollNo = x.RollNo,
                    Gender = x.Gender.ToString(),
                    Email = x.Email,
                    PhoneNo = x.PhoneNo,
                    Section = x.Section,
                    Degree = x.Degree,
                    Batch = x.Batch,
                    CurrentSemester = x.CurrentSemester,
                    Gpa = x.Gpa,
                    Credits = x.Credits,
                    ProfileImageUrl = x.ProfileImageUrl,
                    Department = new DepartmentDTO
                    {
                        Id = x.Department.Id,
                        Name = x.Department.Name,
                        Code = x.Department.Code,
                        Description = x.Department.Description
                    }
                })
                .ToListAsync();
            return studentsDTO;
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
                existingStudent.DepartmentsId = student.DepartmentsId;

                await _uniSphereDbContext.SaveChangesAsync();
                return existingStudent;
            }
            return null;
        }
    }
}
