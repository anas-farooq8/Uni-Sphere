using Uni_Sphere.Models.Domain;

namespace Uni_Sphere.Repositories.IRepositories
{
    public interface IStudentFunctionality
    {
        Task<int> GetStudentIdByUserEmailAsync(string email);

        Task<IEnumerable<Attendance>> GetAttendanceOfCourse(int studentId, int courseId);
    }
}
