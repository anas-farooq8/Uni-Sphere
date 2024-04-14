using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uni_Sphere.Models.Enum;

namespace Uni_Sphere.Models.ViewModels
{
    public class AddTeacherRequest
    {

        [Required, MaxLength(50)]
        public string FullName { get; set; }

        [EmailAddress, MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(6)")]
        public Gender Gender { get; set; }

        [Required, Phone, MaxLength(12)]
        [Column(TypeName = "varchar(12)")]
        public string PhoneNo { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public JobTitle Designation { get; set; }

        [Required, Column(TypeName = "decimal(18, 2)")]
        public decimal Salary { get; set; }

        public string? ProfileImageUrl { get; set; }

        // Display Departments in a dropdown list
        public IEnumerable<SelectListItem>? Departments { get; set; }
        // Selected Department
        [Required]
        public int DepartmentsId { get; set; }
    }
}
