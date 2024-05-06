using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.ViewModels;
using Uni_Sphere.Repositories;
using Uni_Sphere.Repositories.IRepositories;

namespace Uni_Sphere.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "Student")]

    public class StudentAttendanceController(IStudentFunctionality studentFunctionality, IStudentCoursesRepository studentCoursesRepository,
        SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) : Controller
    {
        private readonly IStudentFunctionality _studentFunctionality = studentFunctionality;
        private readonly IStudentCoursesRepository _studentCoursesRepository = studentCoursesRepository;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly UserManager<IdentityUser> _userManager = userManager;


        [HttpGet]
        public async Task<IActionResult> ViewAttendance()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userEmail = (await _userManager.GetUserAsync(User))?.Email;
                if (userEmail != null)
                {
                    var studentId = await _studentFunctionality.GetStudentIdByUserEmailAsync(userEmail);

                    var courses = await _studentCoursesRepository.GetCoursesByStudentAsync(studentId);
                    var firstCourse = courses.FirstOrDefault();

                    var attendance = firstCourse != null
                        ? await _studentFunctionality.GetAttendanceOfCourse(studentId, firstCourse.Id)
                        : new List<Attendance>();

                    var model = new ViewAttendanceRequest
                    {
                        Courses = courses.Select(c => new SelectListItem
                        {
                            Text = $"{c.Code} - {c.Name}",
                            Value = c.Id.ToString()
                        }),
                        Attendances = attendance
                    };

                    return View(model);
                }
            }

            return RedirectToAction("Login", "Account", new { area = "Shared" });
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAttendanceOfCourse(int courseId)
        {
            var userEmail = (await _userManager.GetUserAsync(User))?.Email;
            if (userEmail != null)
            {
                var studentId = await _studentFunctionality.GetStudentIdByUserEmailAsync(userEmail);

                var attendance = await _studentFunctionality.GetAttendanceOfCourse(studentId, courseId);

                var result = attendance.Select(a => new {
                    date = a.Date.ToString("yyyy-MM-dd"),
                    isPresent = a.IsPresent
                });

                return Json(result);
            }

            return Json(new List<object>());
        }

        #endregion
    }
}
