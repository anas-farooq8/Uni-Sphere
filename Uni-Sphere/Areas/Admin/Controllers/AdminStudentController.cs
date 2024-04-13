using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Text.Json;
using Uni_Sphere.DataAccess.Data;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.ViewModels;
using Uni_Sphere.Repositories.IRepositories;

namespace Uni_Sphere.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminStudentController(IStudentRepository studentRepository, IDepartmentRepository departmentRepository) : Controller
    {
        private readonly IStudentRepository _studentRepository = studentRepository;
        private readonly IDepartmentRepository _departmentRepository = departmentRepository;

        private string getBatch()
        {
            // Get the current year. 21, 22, 23, etc.
            int currentYear = DateTime.Now.Year;
            return (currentYear % 100).ToString();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var departments = await _departmentRepository.GetAllAsync();
            var model = new AddStudentRequest
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
        public async Task<IActionResult> Add(AddStudentRequest addStudentRequest)
        {
            if (ModelState.IsValid)
            {
                var count = await _studentRepository.Count();
                var batch = getBatch();
                var rollNo = $"{batch}u-{(count + 1).ToString("D4")}"; // This will give "21u-0001" if the count was 0
                var email = $"{rollNo}@unishere.edu.pk";

                var student = new Students
                {
                    FullName = addStudentRequest.FullName,
                    RollNo = rollNo,
                    Gender = addStudentRequest.Gender,
                    Email = email,
                    PhoneNo = addStudentRequest.PhoneNo,
                    Section = char.ToUpper(addStudentRequest.Section),
                    Degree = addStudentRequest.Degree,
                    Batch = int.Parse(batch),
                    ProfileImageUrl = addStudentRequest.ProfileImageUrl,
                    DepartmentsId = addStudentRequest.DepartmentsId,
                };

                await _studentRepository.AddAsync(student);

                // Create Account (username, email, password)
                var status = await _studentRepository.CreateAccount(email, email, rollNo);
                if (status)
                {
                    TempData["Success"] = "Student added successfully!";
                }
                else
                {
                    TempData["Error"] = "An error occurred while adding the student!";
                }
                return RedirectToAction("List");
            }

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // Read all the students from the database
            var students = await _studentRepository.GetAllAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var departments = await _departmentRepository.GetAllAsync();

            var student = await _studentRepository.GetAsync(id);
            if (student != null)
            {
                var editStudentRequest = new EditStudentRequest
                {
                    Id = student.Id,
                    FullName = student.FullName,
                    RollNo = student.RollNo,
                    Gender = student.Gender,
                    Email = student.Email,
                    PhoneNo = student.PhoneNo,
                    Section = student.Section,
                    Degree = student.Degree,
                    Batch = student.Batch,
                    CurrentSemester = student.CurrentSemester,
                    Gpa = student.Gpa,
                    Credits = student.Credits,
                    ProfileImageUrl = student.ProfileImageUrl,
                    Departments = departments.Select(x => new SelectListItem
                    {
                        Text = x.Code + " - " + x.Name,
                        Value = x.Id.ToString()
                    }),
                    DepartmentsId = student.DepartmentsId,
                };
                return View(editStudentRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditStudentRequest editStudentRequest)
        {
            if (ModelState.IsValid)
            {
                var student = new Students
                {
                    Id = editStudentRequest.Id,
                    FullName = editStudentRequest.FullName,
                    Gender = editStudentRequest.Gender,
                    PhoneNo = editStudentRequest.PhoneNo,
                    Section = char.ToUpper(editStudentRequest.Section),
                    Degree = editStudentRequest.Degree,
                    CurrentSemester = editStudentRequest.CurrentSemester,
                    Gpa = editStudentRequest.Gpa,
                    Credits = editStudentRequest.Credits,
                    ProfileImageUrl = editStudentRequest.ProfileImageUrl,
                    DepartmentsId = editStudentRequest.DepartmentsId,
                };

                var updatedStudent = await _studentRepository.UpdateAsync(student);
                if (updatedStudent != null)
                {
                    TempData["Success"] = "Student updated successfully";
                }
                else
                {
                    TempData["Error"] = "An error occurred while updating the student";
                }
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editStudentRequest.Id });
            //return View();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentRepository.DeleteAsync(id);
            if (student != null)
            {
                await _studentRepository.DeleteAccount(student.Email);
                return Json(new { success = true, message = "Student deleted successfully" });
            }
            else
            {
                return Json(new { success = false, message = "An error occurred while deleting the student" });
            }
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentRepository.GetAllAsync();
            return Json(new { data = students });
        }
        #endregion
    }
}


