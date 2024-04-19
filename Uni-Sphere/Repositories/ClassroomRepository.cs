using Microsoft.EntityFrameworkCore;
using Uni_Sphere.DataAccess.Data;
using Uni_Sphere.DataAccess.Migrations;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.DTO;
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

        public async Task<IEnumerable<ClassroomDTO>> GetAllAsync(int teacherId)
        {
            var classrooms = await _uniSphereDbContext.Classrooms
                .Include(c => c.Course)
                .Include(c => c.Students)
                .Where(c => c.TeacherId == teacherId)
                .Select(c => new ClassroomDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    CourseId = c.CourseId,
                    CourseName = c.Course.Name,
                    TeacherId = c.TeacherId,
                    Batch = c.Batch,
                    TeacherName = c.Teacher.FullName,
                    NoOfStudents = c.Students.Count,
                })
                .ToListAsync();

            return classrooms;
        }

        public async Task<ClassroomDTO> GetClassroomByIdAsync(int classroomId)
        {
            var classroom = await _uniSphereDbContext.Classrooms
                .Include(c => c.Course)
                .Include(c => c.Students)
                .Where(c => c.Id == classroomId) // Filter by classroomId
                .Select(c => new ClassroomDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    CourseId = c.CourseId,
                    CourseName = c.Course.Name,
                    TeacherId = c.TeacherId,
                    Batch = c.Batch,
                    TeacherName = c.Teacher.FullName,
                    NoOfStudents = c.Students.Count,
                    DiscussionPosts = c.DiscussionPosts,
                    Students = c.Students.Select(s => new StudentDTO
                    {
                        Id = s.Id,
                        FullName = s.FullName,
                        RollNo = s.RollNo,
                        Gender = s.Gender.ToString(),
                        Email = s.Email,
                        PhoneNo = s.PhoneNo,
                        Degree = s.Degree,
                        Batch = s.Batch,
                        CurrentSemester = s.CurrentSemester,
                        Gpa = s.Gpa,
                        Credits = s.Credits,
                        ProfileImageUrl = s.ProfileImageUrl,
                        Department = new DepartmentDTO
                        {
                            Id = s.Department.Id,
                            Name = s.Department.Name,
                            Code = s.Department.Code,
                            Description = s.Department.Description
                        },
                        Section = new SectionDTO
                        {
                            Id = s.Section.Id,
                            Name = s.Section.Name
                        }
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return classroom;
        }

        public async Task<bool> AddPostAsync(DiscussionPost post)
        {
            await _uniSphereDbContext.DiscussionPosts.AddAsync(post);
            await _uniSphereDbContext.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            var post = await _uniSphereDbContext.DiscussionPosts.FindAsync(postId);
            if(post != null)
            {
                _uniSphereDbContext.DiscussionPosts.Remove(post);
                await _uniSphereDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<DiscussionPost?> GetPostByIdAsync(int postId)
        {
            var post = await _uniSphereDbContext.DiscussionPosts.FindAsync(postId);
            if(post != null)
                return post;
            else
                return null;
        }

        public async Task<bool> EditPostAsync(DiscussionPost post)
        {
            var existingPost = await _uniSphereDbContext.DiscussionPosts.FindAsync(post.Id);
            if(existingPost != null)
            {
                existingPost.Title = post.Title;
                existingPost.Content = post.Content;
                existingPost.CreatedAt = post.CreatedAt;

                await _uniSphereDbContext.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
