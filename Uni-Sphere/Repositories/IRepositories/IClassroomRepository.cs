using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.DTO;

namespace Uni_Sphere.Repositories.IRepositories
{
    public interface IClassroomRepository
    {
        Task AddClassroomAsync(Classrooms classroom);

    }
}
