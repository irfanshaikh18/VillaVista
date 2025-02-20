using Microsoft.EntityFrameworkCore;
using VillaVista.Domain.Entities;

namespace VillaVista.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Villa> Villas { get; set; }
    }
}
