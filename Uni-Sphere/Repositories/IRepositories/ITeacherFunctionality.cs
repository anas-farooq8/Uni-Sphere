using Uni_Sphere.Models.DTO;

namespace Uni_Sphere.Repositories.IRepositories
{
    public interface ITeacherFunctionality
    {
        Task<IEnumerable<CourseDisplayDTO>> GetCoursesByTeacherAsync(int teacherId);
    }
}
