using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Uni_Sphere.Utility;

namespace Uni_Sphere.DataAccess.Data
{
    public class AuthDbContext: IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed the roles: admin, teacher, student
            // Console.WriteLine(Guid.NewGuid())

            var adminRoleId = "1d315e70-3162-4198-9d84-17592aebe8d0";
            var teacherRoleId = "c3ce6316-d385-4eb0-ae71-b371d23d3d22";
            var studentRoleId = "ce1a268a-e03d-4875-8ed3-4a1c09601294";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = SD.Role_Admin,
                    NormalizedName = SD.Role_Admin,
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = SD.Role_Teacher,
                    NormalizedName = SD.Role_Teacher,
                    Id = teacherRoleId,
                    ConcurrencyStamp = teacherRoleId
                },
                new IdentityRole
                {
                    Name = SD.Role_Student,
                    NormalizedName = SD.Role_Student,
                    Id = studentRoleId,
                    ConcurrencyStamp = studentRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);


            // Seed the admin user
            var adminId = "2082dbd3-90ce-428c-98bd-53deaf2baad6";
            var adminUser = new IdentityUser
            {
                UserName = "admin@unisphere.com",
                Email = "admin@unisphere.com",
                NormalizedEmail = "admin@unisphere.com".ToUpper(),
                NormalizedUserName = "admin@unisphere.com".ToUpper(),
                Id = adminId
            };

            adminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(adminUser, "admin@123");

            builder.Entity<IdentityUser>().HasData(adminUser);

            // Assigning the admin role to the admin user
            var adminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
