using Uni_Sphere.Models.DTO;

namespace Uni_Sphere.Repositories.IRepositories
{
    public interface IStudentCoursesRepository
    {
        Task<IEnumerable<CourseDisplayDTO>> GetCoursesByStudentAsync(int studentId);
        Task<bool> RegisterCoursesAsync(int studentId, List<int> courseIds);
    }
}
