using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni_Sphere.Models.ViewModels
{
    public class RegisterCourseRequest
    {
        // Student Id
        public int StudentId { get; set; }

        // Display Courses in a dropdown list
        public IEnumerable<SelectListItem>? Courses { get; set; }
        // Selected Course
        [Required]
        public int[] CoursesIds { get; set; } = Array.Empty<int>();

    }
}
