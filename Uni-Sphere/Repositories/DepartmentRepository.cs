using Microsoft.EntityFrameworkCore;
using Uni_Sphere.DataAccess.Data;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.DTO;
using Uni_Sphere.Repositories.IRepositories;

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
            // Add the Department
            await _uniSphereDbContext.Departments.AddAsync(department);
            await _uniSphereDbContext.SaveChangesAsync();

            // Fetch all sections
            var sections = await _uniSphereDbContext.Sections.ToListAsync();

            // Associate all sections with the new department
            foreach (var section in sections)
            {
                _uniSphereDbContext.Entry(department).Collection(d => d.Sections).Load();
                department.Sections.Add(section);
            }

            // Save changes
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

        public async Task<IEnumerable<DepartmentDTO>> GetAllAsync()
        {
            var departmentsDTO = await _uniSphereDbContext.Departments.Select(x => new DepartmentDTO
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Description = x.Description
            }).ToListAsync();

            return departmentsDTO;
        }

        public async Task<IEnumerable<SectionDTO>> GetAllSectionsAsync()
        {
            var sectionsDTO = await _uniSphereDbContext.Sections.Select(x => new SectionDTO
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return sectionsDTO;
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
