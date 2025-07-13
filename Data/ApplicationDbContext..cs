using Microsoft.EntityFrameworkCore;
using GestionInterventions.Models.Entities;

namespace GestionInterventions.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // On ajoutera les DbSet plus tard
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Intervention> Interventions { get; set; }
    }
}

/* Ce fichier est la base de données de l'application.
 */