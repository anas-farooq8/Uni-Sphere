using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Uni_Sphere.Models.Domain
{
    public class Teachers
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


        // One to Many Relationship with Departments
        public int DepartmentId { get; set; }

    }
}