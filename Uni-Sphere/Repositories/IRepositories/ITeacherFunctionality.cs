using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.DTO;

namespace Uni_Sphere.Repositories.IRepositories
{
    public interface ITeacherFunctionality
    {
        Task<IEnumerable<CourseDisplayDTO>> GetCoursesByTeacherAsync(int teacherId);
        Task<IEnumerable<Students>> GetStudentsByCourseBatchSectionsAsync(int courseId, int batch, string sections);
        Task<int> GetTeacherIdByUserEmailAsync(string email);
    }
}
