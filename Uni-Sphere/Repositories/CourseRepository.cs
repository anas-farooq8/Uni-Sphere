using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Uni_Sphere.DataAccess.Data;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.DTO;
using Uni_Sphere.Repositories.IRepositories;

namespace Uni_Sphere.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private UniSphereDbContext _uniSphereDbContext;
        public CourseRepository(UniSphereDbContext uniSphereDbContext)
        {
            _uniSphereDbContext = uniSphereDbContext;
        }

        public async Task<Courses> AddAsync(Courses course)
        {
            await _uniSphereDbContext.Courses.AddAsync(course);
            await _uniSphereDbContext.SaveChangesAsync();

            return course;
        }

        public async Task<int> Count()
        {
            return await _uniSphereDbContext.Courses.CountAsync();
        }

        public async Task<Courses?> DeleteAsync(int id)
        {
            var course = await _uniSphereDbContext.Courses.FindAsync(id);
            if (course != null)
            {
                _uniSphereDbContext.Courses.Remove(course);
                await _uniSphereDbContext.SaveChangesAsync();
                return course;
            }
            return null;
        }

        public async Task<IEnumerable<Courses>> GetCoursesByDepartment(int departmentId)
        {
            return await _uniSphereDbContext.Courses
                .Where(c => c.Departments.Any(d => d.Id == departmentId))
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseDTO>> GetAllAsync()
        {
            var coursesDTO = await _uniSphereDbContext.Courses.Select(x => new CourseDTO
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                CreditHours = x.CreditHours,
                CourseType = x.CourseType.ToString(),
                IsLab = x.IsLab,
                Description = x.Description
            }).ToListAsync();

            return coursesDTO;
        }

        public async Task<Courses?> GetAsync(int id)
        {
            return await _uniSphereDbContext.Courses.Include(x => x.Departments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Courses?> UpdateAsync(Courses course)
        {
            var existingCourse = await _uniSphereDbContext.Courses.Include(x => x.Departments).
                FirstOrDefaultAsync(x => x.Id == course.Id);

            if (existingCourse != null)
            {
                existingCourse.Name = course.Name;
                existingCourse.Code = course.Code;
                existingCourse.CreditHours = course.CreditHours;
                existingCourse.CourseType = course.CourseType;
                existingCourse.IsLab = course.IsLab;
                existingCourse.Description = course.Description;
                existingCourse.Departments = course.Departments;

                await _uniSphereDbContext.SaveChangesAsync();
                return existingCourse;
            }

            return null;
        }

        public async Task AssignCourse(TeacherCourseSection teacherCourseSection)
        {
            await _uniSphereDbContext.TeacherCourseSections.AddAsync(teacherCourseSection);
            await _uniSphereDbContext.SaveChangesAsync();
        }
    }
}
