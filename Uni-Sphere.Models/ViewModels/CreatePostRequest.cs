using System.ComponentModel.DataAnnotations;

namespace Uni_Sphere.Models.ViewModels
{
    public class CreatePostRequest
    {
        public int ClassRoomsId { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Content { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
