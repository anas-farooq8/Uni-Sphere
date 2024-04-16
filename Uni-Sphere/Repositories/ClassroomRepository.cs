using Uni_Sphere.DataAccess.Data;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Repositories.IRepositories;

namespace Uni_Sphere.Repositories
{
    public class ClassroomRepository(UniSphereDbContext uniSphereDbContext) : IClassroomRepository
    {
        private readonly UniSphereDbContext _uniSphereDbContext = uniSphereDbContext;

        public async Task AddClassroomAsync(Classrooms classroom)
        {
            await _uniSphereDbContext.Classrooms.AddAsync(classroom);
            await _uniSphereDbContext.SaveChangesAsync();
        }
    }
}
