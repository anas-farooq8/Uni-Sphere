using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Uni_Sphere.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Uni_Sphere.Models.ViewModels
{
    public class AddTeacherRequest
    {

        [Required, MaxLength(50)]
        public string FullName { get; set; }

        [Required, EmailAddress, MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(6)")]
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


        // Display Departments in a dropdown list
        public IEnumerable<SelectListItem> Departments { get; set; }
        // Selected Department
        public int DepartmentId { get; set; }
    }
}
