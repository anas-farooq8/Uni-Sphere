using NuGet.DependencyResolver;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.DTO;

namespace Uni_Sphere.Repositories.IRepositories
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<TeacherDTO>> GetAllAsync();
        Task<Teachers?> GetAsync(int id);
        Task<Teachers> AddAsync(Teachers teacher);
        Task<IEnumerable<Teachers>> GetTeachersByDepartment(int departmentId);
        Task<bool> CreateAccount(string username, string email, string password);
        Task<bool> DeleteAccount(string username);
        Task<Teachers?> UpdateAsync(Teachers teacher);
        Task<Teachers?> DeleteAsync(int id);
        Task<int> Count();
    }
}
