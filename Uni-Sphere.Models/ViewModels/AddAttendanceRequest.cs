
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Uni_Sphere.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using Uni_Sphere.Models.DTO;

namespace Uni_Sphere.Models.ViewModels
{
    public class AddAttendanceRequest
    {
        // students
        public ICollection<StudentDTO> Students { get; set; }

        public int CoursesId { get; set; }
        public IEnumerable<SelectListItem>? Courses { get; set; }

        public int SectionsId { get; set; }
        public IEnumerable<SelectListItem> Sections { get; set; }

        // Map of StudentId, Attendance (1, 0)
        public Dictionary<int, bool> Attendance { get; set; } = new Dictionary<int, bool>();

        public DateOnly Date { get; set; }

        // Just for Displaying
        public string Name { get; set; }
        public string Code { get; set; }
        public int CreditHours { get; set; }
        public string CourseType { get; set; }
        public int Batch { get; set; }
    }

}
