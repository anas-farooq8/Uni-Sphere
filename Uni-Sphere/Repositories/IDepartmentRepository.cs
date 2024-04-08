using Uni_Sphere.Models.Domain;

namespace Uni_Sphere.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Departments>> GetAllAsync();
        Task<Departments?> GetAsync(int id);
        Task<Departments> AddAsync(Departments department);
        Task<Departments?> UpdateAsync(Departments department);
        Task<Departments?> DeleteAsync(int id);
        Task<int> Count();
    }
}
