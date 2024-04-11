using Microsoft.EntityFrameworkCore;
using Uni_Sphere.DataAccess.Data;
using Uni_Sphere.Models.Domain;

namespace Uni_Sphere.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private UniSphereDbContext _uniSphereDbContext;
        public DepartmentRepository(UniSphereDbContext uniSphereDbContext)
        {
            _uniSphereDbContext = uniSphereDbContext;       
        }

        public async Task<Departments> AddAsync(Departments department)
        {
            await _uniSphereDbContext.Departments.AddAsync(department);
            await _uniSphereDbContext.SaveChangesAsync();

            return department;
        }

        public async Task<int> Count()
        {
            return await _uniSphereDbContext.Departments.CountAsync();
        }

        public async Task<Departments?> DeleteAsync(int id)
        {
            var department = await _uniSphereDbContext.Departments.FindAsync(id);
            if (department != null)
            {
                _uniSphereDbContext.Departments.Remove(department);
                await _uniSphereDbContext.SaveChangesAsync();
                return department;
            }
            return null;
        }

        public async Task<IEnumerable<Departments>> GetAllAsync()
        {
            return await _uniSphereDbContext.Departments.ToListAsync();
        }

        public async Task<Departments?> GetAsync(int id)
        {
            return await _uniSphereDbContext.Departments.FindAsync(id);
        }

        public async Task<Departments?> UpdateAsync(Departments department)
        {
            var existingDepartment = await _uniSphereDbContext.Departments.FindAsync(department.Id);
            if (existingDepartment != null)
            {
                existingDepartment.Name = department.Name;
                existingDepartment.Code = department.Code;
                existingDepartment.Description = department.Description;

                await _uniSphereDbContext.SaveChangesAsync();
                return existingDepartment;
            }
            return null;
        }
    }
}
