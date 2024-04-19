using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Uni_Sphere.Models.Domain
{
    public class Classrooms
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ValidateNever]
        public int Batch { get; set; }

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

        // Many-to-many relationship with Students
        public ICollection<Students> Students { get; set; }

        // One-to-many relationship with DiscussionPost
        public ICollection<DiscussionPost> DiscussionPosts { get; set; }

    }
}
