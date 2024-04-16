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
                var selectedDepartments = new List<Departments>();
                foreach(var departmentId in addCourseRequest.selectedDepartments)
                {
                    var department = await _departmentRepository.GetAsync(departmentId);
                    if (department != null)
                    {
                        selectedDepartments.Add(department);
                    }
                }

                course.Departments = selectedDepartments;

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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var departments = await _departmentRepository.GetAllAsync();

            var course = await _courseRepository.GetAsync(id);
            if (course != null)
            {
                var editCourseRequest = new EditCourseRequest
                {
                    Id = course.Id,
                    Code = course.Code,
                    Name = course.Name,
                    CreditHours = course.CreditHours,
                    CourseType = course.CourseType,
                    IsLab = course.IsLab,
                    Description = course.Description,
                    Departments = departments.Select(x => new SelectListItem
                    {
                        Text = x.Code + " - " + x.Name,
                        Value = x.Id.ToString()
                    }),
                    selectedDepartments = course.Departments.Select(x => x.Id).ToArray()
                };
                return View(editCourseRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCourseRequest editCourseRequest)
        {
            if (ModelState.IsValid)
            {
                editCourseRequest.Description ??= "";

                var course = new Courses
                {
                    Id = editCourseRequest.Id,
                    Name = editCourseRequest.Name,
                    Code = editCourseRequest.Code,
                    CreditHours = editCourseRequest.CreditHours,
                    CourseType = editCourseRequest.CourseType,
                    IsLab = editCourseRequest.IsLab,
                    Description = editCourseRequest.Description.Trim(),
                };

                var selectedDepartments = new List<Departments>();
                foreach (var departmentId in editCourseRequest.selectedDepartments)
                {
                    var department = await _departmentRepository.GetAsync(departmentId);
                    if (department != null)
                    {
                        selectedDepartments.Add(department);
                    }
                }
                course.Departments = selectedDepartments;

                var updatedCourse = await _courseRepository.UpdateAsync(course);
                if (updatedCourse != null)
                {
                    TempData["Success"] = "Course updated successfully";
                }
                else
                {
                    TempData["Error"] = "An error occurred while updating the course";
                }
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editCourseRequest.Id });
            //return View();
        }

        // Assign course to teacher
        [HttpGet]
        public async Task<IActionResult> AssignCourse()
        {
            // Display the empty page with select list items
            var departments = await _departmentRepository.GetDepartmentWithInfoAsync(null);
            var firstdepartment = departments.FirstOrDefault();

            var model = new AssignCourseRequest
            {
                Departments = departments.Select(x => new SelectListItem
                {
                    Text = x.Code + " - " + x.Name,
                    Value = x.Id.ToString()
                }),
                Courses = firstdepartment != null ? firstdepartment.Courses.Select(x => new SelectListItem
                {
                    Text = x.Code + " - " + x.Name,
                    Value = x.Id.ToString()
                }) : null,
                Teachers = firstdepartment != null ? firstdepartment.Teachers.Select(x => new SelectListItem
                {
                    Text = x.FullName + " - " + x.Designation,
                    Value = x.Id.ToString()
                }) : null,
                Sections = firstdepartment != null ? firstdepartment.Sections.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }) : null
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignCourse(AssignCourseRequest assignCourseRequest)
        {
            if (ModelState.IsValid)
            {
                // mapping sections from the selected section ids
                foreach (var sectionId in assignCourseRequest.SectionsIds)
                {
                    var teacherCourseSection = new TeacherCourseSection
                    {
                        TeacherId = assignCourseRequest.TeachersId,
                        CourseId = assignCourseRequest.CoursesId,
                        SectionId = sectionId,
                        Batch = assignCourseRequest.Batch
                    };
                    await _courseRepository.AssignCourse(teacherCourseSection);
                }

                TempData["Success"] = "Course assigned successfully";
                return RedirectToAction("AssignCourse");
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetTeachersAndCourses(int departmentId)
        {
            // Fetch teachers and courses based on the selected department
            var teachers = await _teacherRepository.GetTeachersByDepartment(departmentId);
            var courses = await _courseRepository.GetCoursesByDepartment(departmentId);

            // Convert teachers and courses to the SelectListItem format
            var teachersSelectList = teachers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FullName + " - " + x.Designation,
            });

            var coursesSelectList = courses.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Code + " - " + x.Name,
            });

            // Return JSON data containing teachers and courses
            return Json(new { teachers = teachersSelectList, courses = coursesSelectList });
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
