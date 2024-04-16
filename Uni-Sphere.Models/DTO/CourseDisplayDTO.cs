
namespace Uni_Sphere.Models.DTO
{
    public class CourseDisplayDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CreditHours { get; set; }
        public string CourseType { get; set; }
        public bool IsLab { get; set; }
        public string? Description { get; set; }
        public int Batch { get; set; }
        public List<string> Sections { get; set; }
    }
}
