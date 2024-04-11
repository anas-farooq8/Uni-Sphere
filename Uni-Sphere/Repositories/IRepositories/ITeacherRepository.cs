using Uni_Sphere.Models.Domain;

namespace Uni_Sphere.Repositories.IRepositories
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teachers>> GetAllAsync();
        Task<Teachers?> GetAsync(int id);
        Task<Teachers> AddAsync(Teachers teacher);
        Task<bool> CreateAccount(string username, string email, string password);
        Task<bool> DeleteAccount(string username);
        Task<Teachers?> UpdateAsync(Teachers teacher);
        Task<Teachers?> DeleteAsync(int id);
        Task<int> Count();
    }
}
