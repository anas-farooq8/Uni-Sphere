using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Uni_Sphere.Models.Domain
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        public int StudentsId { get; set; }
        [ForeignKey("StudentsId")]
        public Students Student { get; set; }

        public int CoursesId { get; set; }
        [ForeignKey("CoursesId")]
        public Courses Course { get; set; }

        public int SectionsId { get; set; }
        [ForeignKey("SectionsId")]
        public Sections Section { get; set; }

        [Required]
        public bool IsPresent { get; set; }

        public DateOnly Date { get; set; }
    }
}
