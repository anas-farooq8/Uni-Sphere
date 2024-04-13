using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uni_Sphere.Models.Domain
{
    public class Sections
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string Name { get; set; }  // This will store single letters A, B, C, ..., Z

        // Many-to-Many Relationship with Departments
        public ICollection<Departments> Departments { get; set; }
    }
}
