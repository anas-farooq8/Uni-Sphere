using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Uni_Sphere.Data;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.ViewModels;
using Uni_Sphere.Repositories;

namespace Uni_Sphere.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminStudentController(IStudentRepository studentRepository, IDepartmentRepository departmentRepository,
        UserManager<IdentityUser> _userManager) : Controller
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
                Departments = departments.Select(x=> new SelectListItem
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

            // Authorization & Authentication
            var identityUser = new IdentityUser
            {
                UserName = email,
                Email = email,
            };

            var password = rollNo;
            var identityResult = await _userManager.CreateAsync(identityUser, password);

            if(identityResult.Succeeded)
            {
                // Assigning Student Role
                var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, "Student");

                if(roleIdentityResult.Succeeded)
                {
                    // Success
                    return RedirectToAction("List");
                }
            }

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // read all the students from the database
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
            if(updatedStudent != null)
            {
                // success
                return RedirectToAction("List");
            }
            else
            {
                // error
            }

            return RedirectToAction("Edit", new {id = editStudentRequest.Id});
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentRepository.DeleteAsync(id);
            if(student != null)
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


