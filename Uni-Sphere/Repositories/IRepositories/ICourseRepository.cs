using CloudinaryDotNet;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.DTO;

namespace Uni_Sphere.Repositories.IRepositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseDTO>> GetAllAsync();
        Task<Courses?> GetAsync(int id);
        Task<IEnumerable<Courses>> GetCoursesByDepartment(int departmentId);
        Task AssignCourse(TeacherCourseSection teacherCourseSection);
        Task<Courses> AddAsync(Courses course);
        Task<Courses?> UpdateAsync(Courses course);
        Task<Courses?> DeleteAsync(int id);
        Task<int> Count();
    }
}
