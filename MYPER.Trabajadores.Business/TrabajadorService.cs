using MYPER.Trabajadores.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using MYPER.Trabajadores.Entity;
namespace MYPER.Trabajadores.Business
{
    public class TrabajadorService
    {
        private readonly TrabajadoresDbContext _context;

        public TrabajadorService(TrabajadoresDbContext context)
        {
            _context = context;
        }

        public async Task<List<Trabajador>> ObtenerTrabajadoresAsync()
        {
            return await _context.ListarTrabajadoresAsync();
        }
    }
}
