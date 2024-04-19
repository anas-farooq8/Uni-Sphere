using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uni_Sphere.Models.ViewModels;
using Uni_Sphere.Repositories.IRepositories;

namespace Uni_Sphere.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class TeacherCourseController(ITeacherFunctionality teacherFunctionality, SignInManager<IdentityUser> signInManager, 
        UserManager<IdentityUser> userManager) : Controller
    {
        private readonly ITeacherFunctionality _teacherFunctionality = teacherFunctionality;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly UserManager<IdentityUser> _userManager = userManager;

        public async Task<IActionResult> List()
        {
            if(_signInManager.IsSignedIn(User))
            {
                var userEmail = _userManager.GetUserAsync(User).Result.Email;
                // Get the teacher's id who is currently logged-in
                var teacherId = await _teacherFunctionality.GetTeacherIdByUserEmailAsync(userEmail);

                var courses = await _teacherFunctionality.GetCoursesByTeacherAsync(teacherId);

                return View(courses);
            }

            return RedirectToAction("Login", "Account", new { area = "Shared" });
        }
    }
}
