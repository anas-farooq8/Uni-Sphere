using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Uni_Sphere.Models.ViewModels
{
    public class AddCourseRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(12)]
        public string Code { get; set; }

        [Required]
        public int CreditHours { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public CourseType CourseType { get; set; }

        [Required]
        public bool IsLab { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        // Display Departments
        public IEnumerable<SelectListItem>? Departments { get; set; }

        // Selected departments
        [Required]
        public int[] selectedDepartments { get; set; } = Array.Empty<int>();

    }
}
