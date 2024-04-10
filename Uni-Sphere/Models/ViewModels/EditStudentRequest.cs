using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Uni_Sphere.Models.Domain;

namespace Uni_Sphere.Models.ViewModels
{
    public class EditStudentRequest
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FullName { get; set; }

        // 21u-0000
        [MaxLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string RollNo { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(6)")]
        public Gender Gender { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [Required, Phone, MaxLength(12)]
        [Column(TypeName = "varchar(12)")]
        public string PhoneNo { get; set; }

        [Required]
        [Column(TypeName = "char(1)")]
        public char Section { get; set; }

        [Required, MaxLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string Degree { get; set; }

        public int Batch { get; set; }

        public short CurrentSemester { get; set; } = 1;

        public float Gpa { get; set; } = 0.0F;

        public int Credits { get; set; } = 0;

        public string? ProfileImageUrl { get; set; }


        // Display Departments in a dropdown list
        public IEnumerable<SelectListItem> Departments { get; set; }
        // Selected Department
        [Required]
        public int DepartmentsId { get; set; }
    }
}
