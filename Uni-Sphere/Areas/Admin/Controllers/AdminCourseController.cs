using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uni_Sphere.Repositories.IRepositories;
using Uni_Sphere.Repositories;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Uni_Sphere.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminCourseController(ICourseRepository courseRepository, ITeacherRepository teacherRepository, IDepartmentRepository departmentRepository) : Controller
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly ITeacherRepository _teacherRepository = teacherRepository;
        private readonly IDepartmentRepository _departmentRepository = departmentRepository;

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var departments = await _departmentRepository.GetAllAsync();
            var model = new AddCourseRequest
            {
                Departments = departments.Select(x => new SelectListItem
                {
                    Text = x.Code + " - " + x.Name,
                    Value = x.Id.ToString()
                }),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCourseRequest addCourseRequest)
        {
            if (ModelState.IsValid)
            {
                // if the provided description is null, set it to an empty string
                addCourseRequest.Description ??= "";

                var course = new Courses
                {
                    Name = addCourseRequest.Name,
                    Code = addCourseRequest.Code,
                    CreditHours = addCourseRequest.CreditHours,
                    CourseType = addCourseRequest.CourseType,
                    IsLab = addCourseRequest.IsLab,
                    Description = addCourseRequest.Description.Trim(),
                };

                // Mapping departments from the selected department ids
                course.Departments = addCourseRequest.selectedDepartments
                                        .Select(x => new Departments { Id = x }).ToList();

                var newCourse = await _courseRepository.AddAsync(course);
                if (newCourse != null)
                {
                    TempData["Success"] = "Course added successfully";
                }
                else
                {
                    TempData["Error"] = "An error occurred while adding the course";
                }
                return RedirectToAction("List");
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            // Read all the courses from the database
            var courses = await _courseRepository.GetAllAsync();
            return View(courses);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepository.DeleteAsync(id);
            if (course != null)
            {
                return Json(new { success = true, message = "Course deleted successfully" });
            }
            else
            {
                return Json(new { success = false, message = "An error occurred while deleting the course" });
            }

        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseRepository.GetAllAsync();
            return Json(new { data = courses });
        }
        #endregion
    }
}
