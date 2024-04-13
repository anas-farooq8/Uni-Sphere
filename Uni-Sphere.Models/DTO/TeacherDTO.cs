using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni_Sphere.Models.Domain;

namespace Uni_Sphere.Models.DTO
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Designation { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal Salary { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DepartmentDTO Department { get; set; }
    }
}
