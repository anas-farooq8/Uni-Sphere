using Uni_Sphere.Models.Domain;

namespace Uni_Sphere.Repositories
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teachers>> GetAllAsync();
        Task<Teachers?> GetAsync(int id);
        Task<Teachers> AddAsync(Teachers teacher);
        Task<Teachers?> UpdateAsync(Teachers teacher);
        Task<Teachers?> DeleteAsync(int id);
        Task<int> Count();
    }
}
