using Microsoft.EntityFrameworkCore;
using Uni_Sphere.DataAccess.Data;
using Uni_Sphere.Models.DTO;
using Uni_Sphere.Repositories.IRepositories;

namespace Uni_Sphere.Repositories
{
    public class TeacherFunctionality(UniSphereDbContext uniSphereDbContext) : ITeacherFunctionality
    {
        private UniSphereDbContext _uniSphereDbContext = uniSphereDbContext;

        public async Task<IEnumerable<CourseDisplayDTO>> GetCoursesByTeacherAsync(int teacherId)
        {
            try
            {
                var courses = await _uniSphereDbContext.TeacherCourseSections
                    .Where(tcs => tcs.TeacherId == teacherId)
                    .Include(tcs => tcs.Course)
                    .Include(tcs => tcs.Section)
                    .GroupBy(tcs => new { tcs.CourseId, tcs.Batch })
                    .Select(group => new CourseDisplayDTO
                    {
                        Id = group.First().Course.Id,
                        Name = group.First().Course.Name,
                        Code = group.First().Course.Code,
                        CreditHours = group.First().Course.CreditHours,
                        CourseType = group.First().Course.CourseType.ToString(),
                        IsLab = group.First().Course.IsLab,
                        Description = group.First().Course.Description,
                        Batch = group.Key.Batch,
                        Sections = group.Select(tcs => tcs.Section.Name).ToList()
                    })
                    .ToListAsync();

                return courses;
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
                throw new Exception("Error occurred while fetching courses by teacher.", ex);
            }
        }
    }
}
