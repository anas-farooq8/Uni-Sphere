using Microsoft.EntityFrameworkCore;
using Uni_Sphere.Models.Domain;

namespace Uni_Sphere.Data
{
    public class UniSphereDbContext: DbContext
    {
        public UniSphereDbContext(DbContextOptions<UniSphereDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        public DbSet<Students> Students { get; set; }

    }
}
