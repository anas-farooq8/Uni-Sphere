﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Text.Json;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.ViewModels;
using Uni_Sphere.Repositories;
using Uni_Sphere.Repositories.IRepositories;

namespace Uni_Sphere.Areas.Admin.Controllers
{

    [Area("Admin")]
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
                    TempData["Success"] = "Teacher added successfully";
                }
                else
                {
                    TempData["Error"] = "An error occurred while adding the teacher!";
                }
                return RedirectToAction("List");
            }

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // Read all the teachers from the database
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
                    TempData["Success"] = "Teacher updated successfully";
                }
                else
                {
                    TempData["Error"] = "An error occurred while updating the teacher!";
                }
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editTeacherRequest.Id });
            //return View();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _teacherRepository.DeleteAsync(id);
            if (teacher != null)
            {
                await _teacherRepository.DeleteAccount(teacher.Email);
                return Json(new { success = true, message = "Teacher deleted successfully" });
            }
            else
            {
                return Json(new { success = false, message = "An error occurred while deleting the teacher" });
            }
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            return Json(new { data = teachers });
        }
        #endregion
    }
}
