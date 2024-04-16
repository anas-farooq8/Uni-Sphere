using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Uni_Sphere.Models.ViewModels
{
    public class AddClassroomRequest
    {
        public string Name { get; set; }

        // Display Departments in a dropdown list
        public IEnumerable<SelectListItem>? Courses { get; set; }

        // Selected Department
        [Required]
        public int CourseId { get; set; }

        public int Batch { get; set; }

        public string Sections { get; set; }

    }
}
