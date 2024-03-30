using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Uni_Sphere.Models.Domain;

namespace Uni_Sphere.Models.ViewModels
{
    public class EditTeacherRequest
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string FullName { get; set; }

        [EmailAddress]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(6)")]
        public Gender Gender { get; set; }

        [Phone]
        [Column(TypeName = "nvarchar(12)")]
        public string PhoneNo { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Department { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public JobTitle Designation { get; set; }
        public DateTime JoiningDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Salary { get; set; }
    }
}
