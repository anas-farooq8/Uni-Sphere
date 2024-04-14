using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Uni_Sphere.Models.Domain
{
    public class TeacherCourseSection
    {
        [Key]
        public int Id { get; set; }

        // Foreign key for Teacher
        [Required]
        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public Teachers Teacher { get; set; }

        // Foreign key for Course
        [Required]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Courses Course { get; set; }

        // Foreign key for Section
        [Required]
        public int SectionId { get; set; }
        [ForeignKey("SectionId")]
        public Sections Section { get; set; }
    }
}
