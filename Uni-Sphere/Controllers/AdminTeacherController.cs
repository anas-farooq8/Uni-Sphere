using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.ViewModels;
using Uni_Sphere.Repositories;

namespace Uni_Sphere.Controllers
{
    public class AdminTeacherController(ITeacherRepository teacherRepository, IDepartmentRepository departmentRepository) : Controller
    {
        private readonly ITeacherRepository _teacherRepository = teacherRepository;
        private readonly IDepartmentRepository _departmentRepository = departmentRepository;
        

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var departments = await _departmentRepository.GetAllAsync();
            var model = new AddTeacherRequest
            {
                Departments = departments.Select(x => new SelectListItem
                {
                    Text = x.Code + " - " + x.Name,
                    Value = x.Id.ToString()
                })
            };

            return View(model);
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
                Designation = addTeacherRequest.Designation,
                JoiningDate = DateTime.Now,
                Salary = addTeacherRequest.Salary,
                DepartmentId = addTeacherRequest.DepartmentId,
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
            var departments = await _departmentRepository.GetAllAsync();

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
                    Designation = teacher.Designation,
                    JoiningDate = teacher.JoiningDate,
                    Salary = teacher.Salary,
                    Departments = departments.Select(x => new SelectListItem
                    {
                        Text = x.Code + " - " + x.Name,
                        Value = x.Id.ToString()
                    }),
                    DepartmentId = teacher.DepartmentId,
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
                Designation = editTeacherRequest.Designation,
                Salary = editTeacherRequest.Salary,
                DepartmentId = editTeacherRequest.DepartmentId,
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
