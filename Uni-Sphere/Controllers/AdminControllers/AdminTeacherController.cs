using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.ViewModels;
using Uni_Sphere.Repositories;

namespace Uni_Sphere.Controllers.AdminControllers
{

    [Authorize(Roles = "Admin")]
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
            if (ModelState.IsValid)
            {
                var email = addTeacherRequest.Email;

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
                    ProfileImageUrl = addTeacherRequest.ProfileImageUrl,
                    DepartmentsId = addTeacherRequest.DepartmentsId,
                };

                await _teacherRepository.AddAsync(teacher);

                // Create Account (username, email, password)
                var status = await _teacherRepository.CreateAccount(email, email, email);
                if (status)
                {
                    return RedirectToAction("List");
                }
            }

            return RedirectToAction("Add");
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
                    ProfileImageUrl = teacher.ProfileImageUrl,
                    Departments = departments.Select(x => new SelectListItem
                    {
                        Text = x.Code + " - " + x.Name,
                        Value = x.Id.ToString()
                    }),
                    DepartmentsId = teacher.DepartmentsId,
                };
                return View(editTeacherRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTeacherRequest editTeacherRequest)
        {
            if (ModelState.IsValid)
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
                    ProfileImageUrl = editTeacherRequest.ProfileImageUrl,
                    DepartmentsId = editTeacherRequest.DepartmentsId,
                };

                var updatedTeacher = await _teacherRepository.UpdateAsync(teacher);
                if (updatedTeacher != null)
                {
                    // success
                    return RedirectToAction("List");
                }
                else
                {
                    // error
                }
            }

            return RedirectToAction("Edit", new { id = editTeacherRequest.Id });
            //return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _teacherRepository.DeleteAsync(id);
            if (teacher != null)
            {
                await _teacherRepository.DeleteAccount(teacher.Email);
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
