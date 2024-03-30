using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uni_Sphere.Data;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.ViewModels;
using Uni_Sphere.Repositories;

namespace Uni_Sphere.Controllers
{
    public class AdminStudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        public AdminStudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        private string getBatch()
        {
            // Get the current year. 21, 22, 23, etc.
            int currentYear = DateTime.Now.Year;
            return (currentYear % 100).ToString();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
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
                DegreeProgram = addStudentRequest.DegreeProgram,
                Batch = int.Parse(batch),
            };

            await _studentRepository.AddAsync(student);

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
                    DegreeProgram = student.DegreeProgram,
                    Batch = student.Batch,
                    CurrentSemester = student.CurrentSemester,
                    Gpa = student.Gpa,
                    Credits = student.Credits
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
                DegreeProgram = editStudentRequest.DegreeProgram,
                CurrentSemester = editStudentRequest.CurrentSemester,
                Gpa = editStudentRequest.Gpa,
                Credits = editStudentRequest.Credits,
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


