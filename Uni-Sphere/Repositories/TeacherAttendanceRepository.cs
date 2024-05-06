using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Uni_Sphere.DataAccess.Data;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.DTO;
using Uni_Sphere.Repositories.IRepositories;
using static System.Collections.Specialized.BitVector32;

namespace Uni_Sphere.Repositories
{
    public class TeacherAttendanceRepository(UniSphereDbContext uniSphereDbContext) : ITeacherAttendanceRepository
    {
        private UniSphereDbContext _uniSphereDbContext = uniSphereDbContext;


        public async Task<IEnumerable<AttendanceDTO>> GetCoursesByTeacherAndBatchAsync(int teacherId, int batch)
        {
            try
            {
                var courses = await _uniSphereDbContext.TeacherCourseSections
                    .Where(tcs => tcs.TeacherId == teacherId && tcs.Batch == batch)
                    .Include(tcs => tcs.Course)
                    .Include(tcs => tcs.Section)
                    .GroupBy(tcs => new { tcs.CourseId, tcs.Batch })
                    .Select(group => new AttendanceDTO
                    {
                        Id = group.First().Course.Id,
                        Name = group.First().Course.Name,
                        Code = group.First().Course.Code,
                        CreditHours = group.First().Course.CreditHours,
                        CourseType = group.First().Course.CourseType.ToString(),
                        IsLab = group.First().Course.IsLab,
                        Description = group.First().Course.Description,
                        Batch = group.Key.Batch,
                        Sections = group.Select(tcs => new SectionDTO
                        {
                            Id = tcs.Section.Id,
                            Name = tcs.Section.Name
                        }),
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


        public async Task<IEnumerable<StudentDTO>> GetStudentsByCourseSectionsAsync(int courseId, int sectionId, int batch)
        {
            // Query students based on course ID and section ID
            var students = await _uniSphereDbContext.Students
                .Where(s => s.Courses.Any(c => c.Id == courseId) && s.SectionsId == sectionId && s.Batch == batch)
                .Select(s => new StudentDTO
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    RollNo = s.RollNo,
                })
                .ToListAsync();

            return students;
        }

        public async Task<bool> MarkAttendanceAsync(Dictionary<int, bool> AttendanceDic, int courseId, int sectionId, DateOnly date)
        {
            // Creating instances of attendance and add it in the table
            foreach (var student in AttendanceDic)
            {
                var attendance = new Attendance
                {

                    StudentsId = student.Key,
                    CoursesId = courseId,
                    SectionsId = sectionId,
                    Date = date,
                    IsPresent = student.Value
                };

                _uniSphereDbContext.Attendance.Add(attendance);
            }

            return await _uniSphereDbContext.SaveChangesAsync() > 0;

        }
    }

}
