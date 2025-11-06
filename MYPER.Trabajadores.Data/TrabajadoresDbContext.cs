using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MYPER.Trabajadores.Entity;

namespace MYPER.Trabajadores.Data
{
    public class TrabajadoresDbContext : DbContext
    {
        // Constructor requerido por AddDbContext
        public TrabajadoresDbContext(DbContextOptions<TrabajadoresDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trabajador> Trabajadores { get; set; }

        public async Task<List<Trabajador>> ListarTrabajadoresAsync()
        {
            return await Trabajadores
                .FromSqlRaw("EXEC sp_ListarTrabajadores")
                .ToListAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trabajador>().ToTable("Trabajadores");
        }
    }
}
