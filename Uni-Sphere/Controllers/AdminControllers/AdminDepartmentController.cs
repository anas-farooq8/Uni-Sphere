using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.ViewModels;
using Uni_Sphere.Repositories;

namespace Uni_Sphere.Controllers.AdminControllers
{

    [Authorize(Roles = "Admin")]
    public class AdminDepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public AdminDepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDepartmentRequest addDepartmentRequest)
        {
            if (ModelState.IsValid)
            {
                // if the provided description is null, set it to an empty string
                addDepartmentRequest.Description ??= "";

                var department = new Departments
                {
                    Name = addDepartmentRequest.Name,
                    Code = addDepartmentRequest.Code,
                    Description = addDepartmentRequest.Description.Trim(),
                };


                await _departmentRepository.AddAsync(department);
                return RedirectToAction("List");
            }

            return View();
        }



        [HttpGet]
        public async Task<IActionResult> List()
        {
            // Read all the Departments from the database
            var departments = await _departmentRepository.GetAllAsync();
            return View(departments);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentRepository.GetAsync(id);
            if (department != null)
            {
                var editDepartmentRequest = new EditDepartmentRequest
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,

                };
                return View(editDepartmentRequest);
            }

            return View(null);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(EditDepartmentRequest editDepartmentRequest)
        {
            if (ModelState.IsValid)
            {
                // if the provided description is null, set it to an empty string
                editDepartmentRequest.Description ??= "";

                var department = new Departments
                {
                    Id = editDepartmentRequest.Id,
                    Name = editDepartmentRequest.Name,
                    Code = editDepartmentRequest.Code,
                    Description = editDepartmentRequest.Description.Trim(),
                };

                var updatedTeacher = await _departmentRepository.UpdateAsync(department);
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

            // return RedirectToAction("Edit", new { id = editDepartmentRequest.Id });
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _departmentRepository.DeleteAsync(id);
            if (department != null)
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
