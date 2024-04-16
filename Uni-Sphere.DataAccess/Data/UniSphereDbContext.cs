using Microsoft.EntityFrameworkCore;
using Uni_Sphere.Models.Domain;
using static System.Collections.Specialized.BitVector32;

namespace Uni_Sphere.DataAccess.Data
{
    public class UniSphereDbContext: DbContext
    {
        public UniSphereDbContext(DbContextOptions<UniSphereDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed departments
            var departments = new List<Departments>
            {
                new Departments
                {
                    Id = 1,
                    Code = "CS-200I",
                    Name = "Computer Science",
                    Description = "Computer Science is the study of computers and computational systems. Unlike electrical and computer engineers, computer scientists deal mostly with software and software systems; this includes their theory, design, development, and application."
                },
                new Departments
                {
                    Id = 2,
                    Code = "SE-200K",
                    Name = "Software Engineering",
                    Description = "Software engineering is the systematic application of engineering approaches to the development of software. Software engineering is a direct sub-field of engineering and has an overlap with computer science and management science."
                },
                new Departments
                {
                    Id = 3,
                    Code = "DS-200D",
                    Name = "Data Science",
                    Description = "Data science is an inter-disciplinary field that uses scientific methods, processes, algorithms and systems to extract knowledge and insights from structured and unstructured data."

                },
                new Departments
                {
                    Id = 4,
                    Code = "AI-200P",
                    Name = "Artificial Intelligence",
                    Description = "Artificial intelligence (AI) refers to the simulation of human intelligence in machines that are programmed to think like humans and mimic their actions. The term may also be applied to any machine that exhibits traits associated with a human mind such as learning and problem-solving."
                },
                new Departments
                {
                    Id = 5,
                    Code = "CS-200Q",
                    Name = "Cyber Security",
                    Description = "Cybersecurity is the practice of protecting systems, networks, and programs from digital attacks. These attacks are usually aimed at accessing, changing, or destroying sensitive information; extorting money from users; or interrupting normal business processes."
                }
            };
            modelBuilder.Entity<Departments>().HasData(departments);

            // Seed sections A to Z
            var sections = new List<Sections>();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                sections.Add(new Sections { Id = c - 'A' + 1, Name = c.ToString() });
            }
            modelBuilder.Entity<Sections>().HasData(sections);


            // Seed join table data for each department having all sections
            foreach (var department in departments)
            {
                foreach (var section in sections)
                {
                    modelBuilder.Entity("DepartmentsSections")
                        .HasData(new
                        {
                            DepartmentsId = department.Id,
                            SectionsId = section.Id
                        });
                }
            }
        }

        

        public DbSet<Students> Students { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Sections> Sections { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<TeacherCourseSection> TeacherCourseSections { get; set; }
        public DbSet<Classrooms> Classrooms { get; set; }

    }
}
