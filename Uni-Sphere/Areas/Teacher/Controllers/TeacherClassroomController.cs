using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uni_Sphere.Models.ViewModels;
using Uni_Sphere.Repositories.IRepositories;
using Uni_Sphere.Models.Domain;

namespace Uni_Sphere.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class TeacherClassroomController(ITeacherFunctionality teacherFunctionality, IClassroomRepository classroomRepository) : Controller
    {
        private readonly ITeacherFunctionality _teacherFunctionality = teacherFunctionality;
        private readonly IClassroomRepository _classroomRepository = classroomRepository;

        public async Task<IActionResult> Add()
        {
            // Get the teacher's id who is currently logged-in
            var teacherId = 2; // Change this to get the actual teacher's id
            var courses = await _teacherFunctionality.GetCoursesByTeacherAsync(teacherId);
            var firstCourse = courses.FirstOrDefault();

            var model = new AddClassroomRequest
            {
                CourseId = firstCourse.Id,
                Courses = courses.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Batch = firstCourse.Batch,
                Sections = string.Join(", ", firstCourse.Sections)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddClassroomRequest model)
        {
            if (ModelState.IsValid)
            {
                var classroom = new Classrooms
                {
                    Name = model.Name,
                    CourseId = model.CourseId,
                    Batch = model.Batch,
                    TeacherId = 2 // Change this to get the actual teacher's id
                };

                // Retrieve students enrolled in the specified course, batch, and sections
                var students = await _teacherFunctionality.GetStudentsByCourseBatchSectionsAsync(model.CourseId, model.Batch, model.Sections);
                var includedStudents = new List<Students>();

                foreach (var student in students)
                {
                    // Add the student to the classroom
                    includedStudents.Add(student);
                }

                classroom.Students = includedStudents;
                await _classroomRepository.AddClassroomAsync(classroom);

                return RedirectToAction("Home");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseDetails(int courseId)
        {
            var teacherId = 2;
            var courses = await _teacherFunctionality.GetCoursesByTeacherAsync(teacherId);
            // Select the course with the given courseId
            var course = courses.FirstOrDefault(c => c.Id == courseId);

            var batch = course.Batch;
            // Extract the sections
            var sections = course.Sections.ToList();
            // Get the course details

            return Json(new { batch = batch, sections = sections });
        }

    }
}