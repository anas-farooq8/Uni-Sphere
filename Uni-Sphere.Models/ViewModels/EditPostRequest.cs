using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni_Sphere.Models.ViewModels
{
    public class EditPostRequest
    {
        [Required]
        public int Id { get; set; }
        public int ClassRoomsId { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
