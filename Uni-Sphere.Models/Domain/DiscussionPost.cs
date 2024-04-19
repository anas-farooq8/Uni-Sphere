

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uni_Sphere.Models.Domain
{
    public class DiscussionPost
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? Content { get; set; }

        // Many to One RelationShip with Classrooms
        [Required]
        public int ClassroomsId { get; set; }

        [ForeignKey("ClassroomsId")]
        public Classrooms ClassRoom { get; set; }

    }
}
