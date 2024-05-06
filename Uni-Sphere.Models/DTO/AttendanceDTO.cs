using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni_Sphere.Models.DTO
{
    public class AttendanceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CreditHours { get; set; }
        public string CourseType { get; set; }
        public bool IsLab { get; set; }
        public string? Description { get; set; }
        public int Batch { get; set; }  
        public IEnumerable<SectionDTO> Sections { get; set; }
    }
}
