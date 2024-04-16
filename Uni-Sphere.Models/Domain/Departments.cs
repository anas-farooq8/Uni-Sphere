using System.ComponentModel.DataAnnotations;

namespace Uni_Sphere.Models.Domain
{
    public class Departments
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(10)]
        public string Code { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        // Many to One Relationship with Students and Teachers
        public ICollection<Students> Students { get; set; }
        public ICollection<Teachers> Teachers { get; set; }

        // Many to Many Relationship with Sections
        public ICollection<Sections> Sections { get; set; }

        // Many to Many Relationship with Courses
        public ICollection<Courses> Courses { get; set; }

    }
}
