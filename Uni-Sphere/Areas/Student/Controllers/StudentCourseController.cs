using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.ViewModels;
using Uni_Sphere.Repositories;
using Uni_Sphere.Repositories.IRepositories;

namespace Uni_Sphere.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "Student")]
    public class StudentCourseController(IStudentFunctionality studentFunctionality, IStudentCoursesRepository studentCoursesRepository,
        ICourseRepository courseRepository,
        SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) : Controller
    {
        private readonly IStudentFunctionality _studentFunctionality = studentFunctionality;
        private readonly IStudentCoursesRepository _studentCoursesRepository = studentCoursesRepository;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly ICourseRepository _courseRepository = courseRepository;

        [HttpGet]
        public async Task<IActionResult> List()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userEmail = _userManager.GetUserAsync(User).Result.Email;
                // Get the student's id who is currently logged-in
                var studentId = await _studentFunctionality.GetStudentIdByUserEmailAsync(userEmail);

                var courses = await _studentCoursesRepository.GetCoursesByStudentAsync(studentId);

                return View(courses);
            }

            return RedirectToAction("Login", "Account", new { area = "Shared" });
        }

        [HttpGet]
        public async Task<IActionResult> RegisterCourse()
        {
            var courses = await _courseRepository.GetAllAsync();

            var courseList = new List<SelectListItem>();
            foreach (var course in courses)
            {
                courseList.Add(new SelectListItem
                {
                    Text = course.Name,
                    Value = course.Id.ToString()
                });
            }

            var registerCourseRequest = new RegisterCourseRequest
            {
                Courses = courseList
            };

            return View(registerCourseRequest);
        }


        [HttpPost]
        public async Task<IActionResult> RegisterCourse(RegisterCourseRequest registerCourseRequest)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userEmail = (await _userManager.GetUserAsync(User))?.Email;
                var studentId = await _studentFunctionality.GetStudentIdByUserEmailAsync(userEmail);

                registerCourseRequest.StudentId = studentId;

                // Register the selected courses for the student
                bool result = await _studentCoursesRepository.RegisterCoursesAsync(studentId, registerCourseRequest.CoursesIds.ToList());

                if (result)
                {
                    TempData["Success"] = "Courses registered successfully";
                }
                else
                {
                    TempData["Error"] = "An error occurred while registering the courses";
                }

                return RedirectToAction("List");
            }

            return RedirectToAction("Login", "Account", new { area = "Shared" });
        }
    }
}
