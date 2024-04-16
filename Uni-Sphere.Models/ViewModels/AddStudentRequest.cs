using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uni_Sphere.Models.Enum;

namespace Uni_Sphere.Models.ViewModels
{
    public class AddStudentRequest
    {

        [Required, MaxLength(50)]
        public string FullName { get; set; }

        // 21u-0000
        [MaxLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string? RollNo { get; set; }

        [Required]
        [Column(TypeName = "varchar(6)")]
        public Gender Gender { get; set; }

        [MaxLength(50)]
        public string? Email { get; set; }

        [Required, Phone, MaxLength(12)]
        [Column(TypeName = "varchar(12)")]
        public string PhoneNo { get; set; }

        [Required]
        [Column(TypeName = "char(1)")]
        public char Section { get; set; } = 'A';

        [Required, MaxLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string Degree { get; set; }

        public string? ProfileImageUrl { get; set; }

        // Display Departments in a dropdown list
        public IEnumerable<SelectListItem>? Departments { get; set; }
        // Selected Department
        [Required]
        public int DepartmentsId { get; set; }


        // Display Sections in a dropdown list
        public IEnumerable<SelectListItem>? Sections { get; set; }
        // Selected Section
        [Required]
        public int SectionsId { get; set; }
    }
}
