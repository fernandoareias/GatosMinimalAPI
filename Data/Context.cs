using Microsoft.EntityFrameworkCore;

namespace GatosMinimal.API.Data
{
    public class Context : DbContext
    {
        public DbSet<Gato> Gatos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
            => options.UseSqlServer("SUA CONNECTION STRING :)");
    }
}
