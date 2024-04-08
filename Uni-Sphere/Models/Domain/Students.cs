using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uni_Sphere.Models.Domain
{
    public class Students
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FullName { get; set; }

        // 21u-0000
        [Required, MaxLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string RollNo { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(6)")]
        public Gender Gender { get; set; }

        [Required, EmailAddress, MaxLength(50)]
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


        // One to Many Relationship with Departments
        public int DepartmentId { get; set; }

    }
}
