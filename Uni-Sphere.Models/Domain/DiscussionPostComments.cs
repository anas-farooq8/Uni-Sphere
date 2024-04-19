
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uni_Sphere.Models.Domain
{
    public class DiscussionPostComments
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }
        public int DiscussionPostsId { get; set; }

        [ForeignKey("DiscussionPostsId")]
        public DiscussionPost DiscussionPosts { get; set; }

        // asp net users table id
        public Guid UserId { get; set; }


        //date added
        public DateTime DateAdded { get; set; }

    }
}
