using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Uni_Sphere.Models.Domain;

namespace Uni_Sphere.Models.ViewModels
{
    public class EditTeacherRequest
    {
        [Key]
        public int Id { get; set; }

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

        public DateTime JoiningDate { get; set; }

        [Required, Column(TypeName = "decimal(18, 2)")]
        public decimal Salary { get; set; }

        public string? ProfileImageUrl { get; set; }

        // Display Departments in a dropdown list
        public IEnumerable<SelectListItem> Departments { get; set; }
        // Selected Department
        [Required]
        public int DepartmentsId { get; set; }
    }
}
