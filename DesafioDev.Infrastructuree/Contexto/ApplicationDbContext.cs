using DesafioDev.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioDev.API.Contexto
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}