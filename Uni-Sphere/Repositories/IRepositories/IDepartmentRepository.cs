using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.DTO;

namespace Uni_Sphere.Repositories.IRepositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentDTO>> GetAllAsync();
        Task<Departments?> GetAsync(int id);
        Task<Departments> AddAsync(Departments department);
        Task<Departments?> UpdateAsync(Departments department);
        Task<Departments?> DeleteAsync(int id);
        Task<int> Count();
    }
}
