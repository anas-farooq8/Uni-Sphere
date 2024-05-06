using Uni_Sphere.Models.DTO;

namespace Uni_Sphere.Repositories.IRepositories
{
    public interface ITeacherAttendanceRepository
    {
        // get all the courses taught by the teacher
        Task<IEnumerable<AttendanceDTO>> GetCoursesByTeacherAndBatchAsync(int teacherId, int batch);
        Task<IEnumerable<StudentDTO>> GetStudentsByCourseSectionsAsync(int courseId, int sections, int batch);
        Task<bool> MarkAttendanceAsync(Dictionary<int, bool> AttendanceDic, int courseId, int sectionId, DateOnly date);
    }
}
