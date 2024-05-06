using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Uni_Sphere.Repositories.IRepositories;
using Uni_Sphere.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Uni_Sphere.Models.DTO;

namespace Uni_Sphere.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class TeacherAttendanceController(ITeacherFunctionality teacherFunctionality, ITeacherAttendanceRepository teacherAttendanceRepository,
        SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) : Controller
    {
        private readonly ITeacherFunctionality _teacherFunctionality = teacherFunctionality;
        private readonly ITeacherAttendanceRepository _teacherAttendanceRepository = teacherAttendanceRepository;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly UserManager<IdentityUser> _userManager = userManager;

        [HttpGet]
        public async Task<IActionResult> MarkAttendance()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userEmail = userManager.GetUserAsync(User).Result.Email;
                // Get the teacher's id who is currently logged-in
                var teacherId = await _teacherFunctionality.GetTeacherIdByUserEmailAsync(userEmail);

                var batch = DateTime.Now.Year % 100;
                // Get all the courses taught by the teacher
                var courses = await _teacherAttendanceRepository.GetCoursesByTeacherAndBatchAsync(teacherId, batch);
                var firstCourse = courses.FirstOrDefault();

                var sections = new List<SelectListItem>();

                // Get the students of the first course
                IEnumerable<StudentDTO> students = new List<StudentDTO>();
                if(firstCourse != null)
                {
                    if (firstCourse.Sections.Any())
                    {
                        sections = firstCourse.Sections.Select(x => new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        }).ToList();

                        students = await _teacherAttendanceRepository.GetStudentsByCourseSectionsAsync(firstCourse.Id, firstCourse.Sections.FirstOrDefault().Id, firstCourse.Batch);
                    }
                }

                // Display the first course's sections
                var model = new AddAttendanceRequest
                {   
                    Courses = courses.Select(x => new SelectListItem
                    {
                        Text = x.Code + " - " + x.Name,
                        Value = x.Id.ToString()
                    }),
                    Sections = sections,
                    Students = (ICollection<StudentDTO>)(students),

                    // Displaying Course Details
                    Name = firstCourse.Name ?? "",
                    Code = firstCourse.Code ?? "",
                    CreditHours = firstCourse.CreditHours,
                    CourseType = firstCourse.CourseType ?? "",
                    Batch = firstCourse.Batch
                };

                return View(model);
            }

            return RedirectToAction("Login", "Account", new { area = "Shared" });
        }

        [HttpPost]
        public async Task<IActionResult> MarkAttendance(AddAttendanceRequest addAttendanceRequest)
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _teacherAttendanceRepository.MarkAttendanceAsync(addAttendanceRequest.Attendance, 
                    addAttendanceRequest.CoursesId, addAttendanceRequest.SectionsId, addAttendanceRequest.Date);

                TempData["success"] = "Attendance Marked Successfully";
                return RedirectToAction("MarkAttendance");

            }
            return RedirectToAction("Login", "Account", new { area = "Shared" });
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> UpdateStudentsSectionsCourses(int batch)
        {
            var userEmail = userManager.GetUserAsync(User).Result.Email;
            // Get the teacher's id who is currently logged-in
            var teacherId = await _teacherFunctionality.GetTeacherIdByUserEmailAsync(userEmail);

            var allCourses = await _teacherAttendanceRepository.GetCoursesByTeacherAndBatchAsync(teacherId, batch);

            var courses = allCourses.Select(x => new SelectListItem
            {
                Text = x.Code + " - " + x.Name,
                Value = x.Id.ToString()
            });

            // Select the course by courseId and batch
            var course = allCourses.FirstOrDefault();

            var sections = new List<SelectListItem>();
            if(course != null)
            {
                sections = course.Sections.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            }

            IEnumerable<StudentDTO> students = new List<StudentDTO>();

            if (course != null)
            {
                if (course.Sections.Any())
                    students = await _teacherAttendanceRepository.GetStudentsByCourseSectionsAsync(course.Id, course.Sections.FirstOrDefault().Id, course.Batch);

            }

            return Json(new { courses, students, sections, course });

        }


        // Update the students and sections based on the course change
        public async Task<IActionResult> UpdateStudentsSections(int courseId, int batch)
        {
            var userEmail = userManager.GetUserAsync(User).Result.Email;
            // Get the teacher's id who is currently logged-in
            var teacherId = await _teacherFunctionality.GetTeacherIdByUserEmailAsync(userEmail);

            var courses = await _teacherAttendanceRepository.GetCoursesByTeacherAndBatchAsync(teacherId, batch);

            // Select the course by courseId and batch
            var course = courses.FirstOrDefault(x => x.Id == courseId);

            // select the sections of the course
            var sections = new List<SelectListItem>();
            if(course != null)
            {
                sections = course.Sections.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            }

            IEnumerable<StudentDTO> students = new List<StudentDTO>();

            if (course != null)
            {
                if(course.Sections.Any())
                    students = await _teacherAttendanceRepository.GetStudentsByCourseSectionsAsync(course.Id, course.Sections.FirstOrDefault().Id, course.Batch);

            }

            return Json(new { course, students, sections });
        }

        // Update the students based on the course, batch, and section
        public async Task<IActionResult> UpdateStudents(int courseId, int batch, int sectionId)
        {

            var students = await _teacherAttendanceRepository.GetStudentsByCourseSectionsAsync(courseId, sectionId, batch) ?? new List<StudentDTO>();

            return Json(new { students });
        }
        #endregion
    }
}
