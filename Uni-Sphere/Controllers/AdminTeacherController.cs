using Microsoft.AspNetCore.Mvc;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.ViewModels;
using Uni_Sphere.Repositories;

namespace Uni_Sphere.Controllers
{
    public class AdminTeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        public AdminTeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTeacherRequest addTeacherRequest)
        {
            var teacher = new Teachers
            {
                FullName = addTeacherRequest.FullName,
                Email = addTeacherRequest.Email,
                Gender = addTeacherRequest.Gender,
                PhoneNo = addTeacherRequest.PhoneNo,
                DateOfBirth = addTeacherRequest.DateOfBirth,
                Department = addTeacherRequest.Department,
                Designation = addTeacherRequest.Designation,
                Salary = addTeacherRequest.Salary,
            };

            await _teacherRepository.AddAsync(teacher);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // read all the students from the database
            var teachers = await _teacherRepository.GetAllAsync();
            return View(teachers);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await _teacherRepository.GetAsync(id);
            if (teacher != null)
            {
                var editTeacherRequest = new EditTeacherRequest
                {
                    Id = teacher.Id,
                    FullName = teacher.FullName,
                    Email = teacher.Email,
                    Gender = teacher.Gender,
                    PhoneNo = teacher.PhoneNo,
                    DateOfBirth = teacher.DateOfBirth,
                    Department = teacher.Department,
                    Designation = teacher.Designation,
                    Salary = teacher.Salary
                };
                return View(editTeacherRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTeacherRequest editTeacherRequest)
        {
            var teacher = new Teachers
            {
                Id = editTeacherRequest.Id,
                FullName = editTeacherRequest.FullName,
                Gender = editTeacherRequest.Gender,
                PhoneNo = editTeacherRequest.PhoneNo,
                DateOfBirth = editTeacherRequest.DateOfBirth,
                Department = editTeacherRequest.Department,
                Designation = editTeacherRequest.Designation,
                Salary = editTeacherRequest.Salary,
            };

            var updatedTeacher= await _teacherRepository.UpdateAsync(teacher);
            if (updatedTeacher != null)
            {
                // success
                return RedirectToAction("List");
            }
            else
            {
                // error
            }

            return RedirectToAction("Edit", new { id = editTeacherRequest.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _teacherRepository.DeleteAsync(id);
            if (teacher != null)
            {
                // success
            }
            else
            {
                // error
            }

            return RedirectToAction("List");
        }

    }
}
