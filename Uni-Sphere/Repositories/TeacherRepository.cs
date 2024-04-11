using Uni_Sphere.DataAccess.Data;
using Uni_Sphere.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Uni_Sphere.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private UniSphereDbContext _uniSphereDbContext;
        private UserManager<IdentityUser> _userManager;

        public TeacherRepository(UniSphereDbContext uniSphereDbContext, UserManager<IdentityUser> userManager)
        {
            _uniSphereDbContext = uniSphereDbContext;
            _userManager = userManager;
        }

        public async Task<Teachers> AddAsync(Teachers teacher)
        {
            await _uniSphereDbContext.Teachers.AddAsync(teacher);
            await _uniSphereDbContext.SaveChangesAsync();

            return teacher;
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
                // Assigning Teacher Role
                var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, "Teacher");

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
                if(result.Succeeded && result is not null)
                {
                    return true;
                }
            }
            return false;
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
                existingTeacher.DepartmentsId = teacher.DepartmentsId;

                await _uniSphereDbContext.SaveChangesAsync();
                return existingTeacher;
            }
            return null;
        }
    }
}
