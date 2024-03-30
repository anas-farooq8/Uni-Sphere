using Microsoft.EntityFrameworkCore;

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

    }
}
