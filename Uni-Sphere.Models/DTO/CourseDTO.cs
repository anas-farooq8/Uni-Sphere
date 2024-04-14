
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Uni_Sphere.Models.Enum;

namespace Uni_Sphere.Models.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CreditHours { get; set; }
        public string CourseType { get; set; }
        public bool IsLab { get; set; }
        public string? Description { get; set; }
    }
}
