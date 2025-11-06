using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using MYPER.Trabajadores.Data;
using MYPER.Trabajadores.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MYPER.Trabajadores.Web.Controllers
{
    public class TrabajadorController : Controller
    {
        private readonly TrabajadoresDbContext _context;

        public TrabajadorController(TrabajadoresDbContext context)
        {
            _context = context;
        }

        // 🔄 Cargar listas para dropdowns
        private void CargarListas()
        {
            ViewBag.Sexos = new List<SelectListItem>
            {
                new SelectListItem { Text = "Masculino", Value = "Masculino" },
                new SelectListItem { Text = "Femenino", Value = "Femenino" }
            };

            ViewBag.TiposDocumento = new List<SelectListItem>
            {
                new SelectListItem { Text = "DNI", Value = "DNI" },
                new SelectListItem { Text = "Carnet de Extranjería", Value = "Carnet de Extranjería" },
                new SelectListItem { Text = "Pasaporte", Value = "Pasaporte" }
            };
        }

        // Listado usando procedimiento almacenado
        //public async Task<IActionResult> Index()
        //{
        //    var trabajadores = await _context.Trabajadores
        //        .FromSqlRaw("EXEC sp_ListarTrabajadores")
        //        .ToListAsync();

        //    return View(trabajadores);
        //}
        public async Task<IActionResult> Index(string filtro, string sexo)
        {
            var trabajadores = await _context.ListarTrabajadoresAsync();

            // Filtros aplicados después del SP
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                trabajadores = trabajadores.Where(t =>
                    (t.Nombres != null && t.Nombres.ToLower().Contains(filtro)) ||
                    (t.Apellidos != null && t.Apellidos.ToLower().Contains(filtro)) ||
                    (t.NumeroDocumento != null && t.NumeroDocumento.ToLower().Contains(filtro))
                ).ToList();
            }

            if (!string.IsNullOrEmpty(sexo))
            {
                trabajadores = trabajadores.Where(t => t.Sexo == sexo).ToList();
            }

            return View(trabajadores);
        }

        //public async Task<IActionResult> Index(string filtro, string sexo)
        //{
        //    var trabajadores = await _context.Trabajadores
        //        .FromSqlRaw("EXEC sp_ListarTrabajadores")
        //        .ToListAsync();

        //    if (!string.IsNullOrEmpty(filtro))
        //    {
        //        filtro = filtro.ToLower();
        //        trabajadores = trabajadores.Where(t =>
        //            (t.Nombres != null && t.Nombres.ToLower().Contains(filtro)) ||
        //            (t.Apellidos != null && t.Apellidos.ToLower().Contains(filtro)) ||
        //            (t.NumeroDocumento != null && t.NumeroDocumento.ToLower().Contains(filtro))
        //        ).ToList();
        //    }

        //    if (!string.IsNullOrEmpty(sexo))
        //    {
        //        trabajadores = trabajadores.Where(t => t.Sexo == sexo).ToList();
        //    }

        //    return View(trabajadores);
        //}


        // GET: Crear
        [HttpGet]
        public IActionResult Create()
        {
            CargarListas();
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Trabajador trabajador)
        //{
        //    //if (ModelState.IsValid)
        //    if (trabajador != null)
        //    {
        //        var sql = "EXEC sp_RegistrarTrabajador @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8";
        //        await _context.Database.ExecuteSqlRawAsync(sql,
        //            trabajador.Nombres,
        //            trabajador.Apellidos,
        //            trabajador.TipoDocumento,
        //            trabajador.NumeroDocumento,
        //            trabajador.Sexo,
        //            trabajador.FechaNacimiento, // Asegúrate que sea DateTime
        //            trabajador.FotoRuta ?? "",
        //            trabajador.Direccion ?? "",
        //            true // Estado activo
        //        );

        //        return Ok(); // AJAX espera esto
        //    }

        //    CargarListas();
        //    return PartialView("CreatePartial", trabajador);
        //}

        // POST: Crear       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trabajador trabajador, IFormFile Foto)
        {
            if (Foto != null && Foto.Length > 0)
            {
                var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(Foto.FileName);
                var rutaFisica = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", nombreArchivo);

                using (var stream = new FileStream(rutaFisica, FileMode.Create))
                {
                    await Foto.CopyToAsync(stream);
                }

                trabajador.FotoRuta = "/uploads/" + nombreArchivo;
            }

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_RegistrarTrabajador @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8",
                trabajador.Nombres,
                trabajador.Apellidos,
                trabajador.TipoDocumento,
                trabajador.NumeroDocumento,
                trabajador.Sexo,
                trabajador.FechaNacimiento,
                trabajador.FotoRuta ?? "",
                trabajador.Direccion ?? "",
                true
            );

            TempData["Mensaje"] = "Trabajador registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Editar
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador == null) return NotFound();

            CargarListas();
            return View(trabajador);
        }

        // POST: Editar
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Trabajador trabajador)
        //{
        //    if (id != trabajador.Id) return NotFound();

        //    //if (ModelState.IsValid)
        //    if(trabajador != null)
        //    {
        //        _context.Update(trabajador);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    CargarListas();
        //    return View(trabajador);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Trabajador trabajador, IFormFile Foto)
        //{
        //    if (id != trabajador.Id) return NotFound();

        //    if (ModelState.IsValid)
        //    {
        //        var trabajadorExistente = await _context.Trabajadores.FindAsync(id);
        //        if (trabajadorExistente == null) return NotFound();

        //        // Actualizar campos básicos
        //        trabajadorExistente.Nombres = trabajador.Nombres;
        //        trabajadorExistente.Apellidos = trabajador.Apellidos;
        //        trabajadorExistente.TipoDocumento = trabajador.TipoDocumento;
        //        trabajadorExistente.NumeroDocumento = trabajador.NumeroDocumento;
        //        trabajadorExistente.Sexo = trabajador.Sexo;
        //        trabajadorExistente.FechaNacimiento = trabajador.FechaNacimiento;
        //        trabajadorExistente.Direccion = trabajador.Direccion;

        //        // Si se subió una nueva foto
        //        if (Foto != null && Foto.Length > 0)
        //        {
        //            var nombreArchivo = Path.GetFileName(Foto.FileName);
        //            var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
        //            Directory.CreateDirectory(rutaCarpeta); // Asegura que la carpeta exista

        //            var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);
        //            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        //            {
        //                await Foto.CopyToAsync(stream);
        //            }

        //            // Guardar ruta relativa
        //            trabajadorExistente.FotoRuta = "/imagenes/" + nombreArchivo;
        //        }

        //        _context.Update(trabajadorExistente);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    CargarListas();
        //    return View(trabajador);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trabajador trabajador, IFormFile Foto, string FotoRuta)
        {
            if (id != trabajador.Id)
                return NotFound();

            if (trabajador != null)
            {
                try
                {
                    // Procesar nueva imagen si existe
                    if (Foto != null && Foto.Length > 0)
                    {
                        var nombreArchivo = Path.GetFileName(Foto.FileName);
                        var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fotos");

                        // Crear carpeta si no existe
                        if (!Directory.Exists(rutaCarpeta))
                            Directory.CreateDirectory(rutaCarpeta);

                        var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                        {
                            await Foto.CopyToAsync(stream);
                        }

                        trabajador.FotoRuta = "/fotos/" + nombreArchivo;
                    }
                    else
                    {
                        // Conservar la foto anterior recibida como parámetro
                        trabajador.FotoRuta = FotoRuta;
                    }

                    _context.Update(trabajador);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Trabajadores.Any(e => e.Id == trabajador.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(trabajador);
        }


        // GET: Eliminar
        public async Task<IActionResult> Delete(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador == null) return NotFound();
            return View(trabajador);
        }

        // POST: Confirmar eliminación
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador != null)
            {
                _context.Trabajadores.Remove(trabajador);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreatePartial()
        {
            CargarListas(); // Asegúrate de que ViewBag.TiposDocumento y ViewBag.Sexos estén cargados
            return PartialView("CreatePartial", new Trabajador());
        }


    }
}
