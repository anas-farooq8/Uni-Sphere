using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Uni_Sphere.Models.Domain;

namespace Uni_Sphere.Models.ViewModels
{
    public class AssignCourseRequest
    {
        // Batch
        [Required]
        public int Batch { get; set; }

        // Display Departments in a dropdown list
        public IEnumerable<SelectListItem>? Departments { get; set; }
        // Selected Department
        [Required]
        public int DepartmentsId { get; set; }

        // Display Courses in a dropdown list
        public IEnumerable<SelectListItem>? Courses { get; set; }
        // Selected Course
        [Required]
        public int CoursesId { get; set; }

        // Display Teachers in a dropdown list
        public IEnumerable<SelectListItem>? Teachers { get; set; }
        // Selected Teacher
        [Required]
        public int TeachersId { get; set; }

        // Display Sections in a dropdown list
        public IEnumerable<SelectListItem>? Sections { get; set; }
        // multiple section choose
        public int[] SectionsIds { get; set; } = Array.Empty<int>();

    }
}
