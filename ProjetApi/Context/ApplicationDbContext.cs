using Microsoft.EntityFrameworkCore;
using ProjetApi.Entities;

namespace ProjetApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Element> Elements { get; set; }

        public DbSet<Faiblesse> Faiblesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Créer la clée étrangère unique vers unique de Carte vers Carte
            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.evolution_)
                .WithOne();

            // Créer la clée étrangère unique vers plusieur de Carte vers Element
            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.element)
                .WithMany(e => e.pokemon)
                .HasForeignKey(p => p.element_name);
        }
    }
}