using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni_Sphere.Models.Domain;

namespace Uni_Sphere.Models.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string RollNo { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Degree { get; set; }
        public int Batch { get; set; }
        public short CurrentSemester { get; set; }
        public double Gpa { get; set; }
        public int Credits { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DepartmentDTO Department { get; set; }
        public SectionDTO Section { get; set; }
    }
}
