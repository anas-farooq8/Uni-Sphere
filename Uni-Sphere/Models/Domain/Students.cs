﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uni_Sphere.Models.Domain
{
    public class Students
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FullName { get; set; }

        // 21u-0000
        [MaxLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string RollNo { get; set; }

        [Required]
        [Column(TypeName = "varchar(6)")]
        public Gender Gender { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [Required, Phone, MaxLength(12)]
        [Column(TypeName = "varchar(12)")]
        public string PhoneNo { get; set; }

        [Required]
        [Column(TypeName = "char(1)")]
        public char Section { get; set; }

        [Required, MaxLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string Degree { get; set; }

        public int Batch { get; set; }

        public short CurrentSemester { get; set; } = 1;

        // Custom error message
        [Range(0.0, 4.0, ErrorMessage = "GPA must be between 0.0 and 4.0")]
        public double Gpa { get; set; } = 0.00;

        public int Credits { get; set; } = 0;

        public string? ProfileImageUrl { get; set; }

        // One to Many Relationship with Departments
        [Required]
        public int DepartmentsId { get; set; }

    }
}
