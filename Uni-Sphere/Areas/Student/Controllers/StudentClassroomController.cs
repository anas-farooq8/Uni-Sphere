using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Uni_Sphere.Repositories.IRepositories;

namespace Uni_Sphere.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "Student")]
    public class StudentClassroomController(IStudentFunctionality studentFunctionality, IClassroomRepository classroomRepository,
        SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) : Controller
    {
        private readonly IStudentFunctionality _studentFunctionality = studentFunctionality;
        private readonly IClassroomRepository _classroomRepository = classroomRepository;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly UserManager<IdentityUser> _userManager = userManager;

        [HttpGet]
        // Display all the classroom of the student
        public async Task<IActionResult> List()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userEmail = _userManager.GetUserAsync(User).Result.Email;
                // Get the teacher's id who is currently logged-in
                var studentId = await _studentFunctionality.GetStudentIdByUserEmailAsync(userEmail);
                var model = await _classroomRepository.GetAllByStudentIdAsync(studentId);

                return View(model);
            }

            return RedirectToAction("Login", "Account", new { area = "Shared" });

        }

        [HttpGet]
        // View Individual Classroom
        public async Task<IActionResult> Classroom(int classroomId)
        {
            var model = await _classroomRepository.GetClassroomByIdAsync(classroomId);

            return View(model);
        }

        [HttpGet]
        // View students in the classroom
        public async Task<IActionResult> DisplayStudents(int classroomId)
        {
            var model = await _classroomRepository.GetClassroomByIdAsync(classroomId);
            return View(model.Students);
        }
    }
}
