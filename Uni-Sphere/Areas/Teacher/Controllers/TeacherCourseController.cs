using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Uni_Sphere.Repositories.IRepositories;

namespace Uni_Sphere.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class TeacherCourseController(ITeacherFunctionality teacherFunctionality) : Controller
    {
        private readonly ITeacherFunctionality _teacherFunctionality = teacherFunctionality;

        public async Task<IActionResult> List()
        {
            // Get the teacher's id who is currently logged-in
            var teacherId = 1;
            var courses = await _teacherFunctionality.GetCoursesByTeacherAsync(teacherId);

            return View(courses);
        }
    }
}
