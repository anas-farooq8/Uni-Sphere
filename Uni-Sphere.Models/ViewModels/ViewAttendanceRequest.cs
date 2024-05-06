using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni_Sphere.Models.Domain;
using Uni_Sphere.Models.DTO;

namespace Uni_Sphere.Models.ViewModels
{
    public class ViewAttendanceRequest
    {
        // IEnumerable of Attendance
        public IEnumerable<Attendance> Attendances { get; set; } = new List<Attendance>();

        // IEnumerable of Courses
        public IEnumerable<SelectListItem>? Courses { get; set; }

        // Selected Course
        public int CoursesId { get; set; }

    }
}
