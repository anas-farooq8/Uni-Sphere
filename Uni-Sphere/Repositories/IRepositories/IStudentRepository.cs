using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.DTO;

namespace Uni_Sphere.Repositories.IRepositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentDTO>> GetAllAsync();
        Task<Students?> GetAsync(int id);
        Task<Students> AddAsync(Students student);
        Task<bool> CreateAccount(string username, string email, string password);
        Task<bool> DeleteAccount(string username);
        Task<Students?> UpdateAsync(Students student);
        Task<Students?> DeleteAsync(int id);
        Task<int> Count();
    }
}
