using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.DTO;

namespace Uni_Sphere.Repositories.IRepositories
{
    public interface IClassroomRepository
    {
        Task AddClassroomAsync(Classrooms classroom);
        Task<IEnumerable<ClassroomDTO>> GetAllAsync(int teacherId);
        Task<IEnumerable<ClassroomDTO>> GetAllByStudentIdAsync(int studentId);
        Task<ClassroomDTO> GetClassroomByIdAsync(int classroomId);
        Task<bool> AddPostAsync(DiscussionPost post);
        Task<bool> DeletePostAsync(int postId);
        Task<DiscussionPost?> GetPostByIdAsync(int postId);
        Task<bool> EditPostAsync(DiscussionPost post);
    }
}
