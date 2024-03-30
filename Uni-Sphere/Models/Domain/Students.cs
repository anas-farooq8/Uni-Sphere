using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uni_Sphere.Models.Domain
{
    public class Students
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string FullName { get; set; }

        // 21u-0000
        [Column(TypeName = "nvarchar(8)")]
        public string RollNo { get; set; }

        [Column(TypeName = "nvarchar(6)")]
        public Gender Gender { get; set; }

        [EmailAddress]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Phone]
        [Column(TypeName = "nvarchar(12)")]
        public string PhoneNo { get; set; }

        [Column(TypeName = "char")]
        public char Section { get; set; }

        [Column(TypeName = "nvarchar(2)")]
        public string Degree { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string DegreeProgram { get; set; }

        public int Batch { get; set; }

        public short CurrentSemester { get; set; } = 1;

        public float Gpa { get; set; } = 0.0F;

        public int Credits { get; set; } = 0;


    }
}
