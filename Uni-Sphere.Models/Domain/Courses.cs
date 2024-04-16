
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Uni_Sphere.Models.Enum;

namespace Uni_Sphere.Models.Domain
{
    public class Courses
    {
        [Key]
        public int Id { get; set; }

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

        [MaxLength(1000)]
        public string? Description { get; set; }

        // Many-to-Many Relationship with Departments
        public ICollection<Departments> Departments { get; set; }

        // Many-to-Many Relationship with Students
        public ICollection<Students> Students { get; set; }

    }
}
