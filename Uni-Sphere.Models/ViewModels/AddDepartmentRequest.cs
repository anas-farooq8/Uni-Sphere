using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uni_Sphere.Models.ViewModels
{
    public class AddDepartmentRequest
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(10)]
        public string Code { get; set; }

        [MaxLength(350)]
        public string? Description { get; set; }
    }
}
