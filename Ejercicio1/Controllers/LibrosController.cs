using Ejercicio1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly biblioContext _libroContext;
        public LibrosController(biblioContext libroContexto)
        {
            _libroContext = libroContexto;
        }
        /// <summary>
        ///  Endpoint que retorna
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Libro> listadoLibro = (from e in _libroContext.libro
                                        select e).ToList();
            if (listadoLibro.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoLibro);
        }
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            var Libro = (from e in _libroContext.libro
                         join A in _libroContext.Autor
                         on e.AutorId equals A.id
                         where e.Id == id
                         select new
                         {
                             e.Id,
                             e.Titulo,
                             e.AñoPublicacion,
                             e.AutorId,
                             e.CategoriaId,
                             e.Resumen,
                             
                             AutorNombre = A.Nombre
                         }).FirstOrDefault();
            if (Libro == null)
            {
                return NotFound();
            }
            return Ok(Libro);
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody] Libro Libro)
        {
            try
            {
                _libroContext.libro.Add(Libro);
                _libroContext.SaveChanges();
                return Ok(Libro);
            }//Esto es para entender mejor el error(solo da mas info)
            catch (DbUpdateException dbEx)
            {
                return BadRequest(dbEx.InnerException?.Message ?? dbEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEquipo(int id, [FromBody] Libro LibroModificar)
        {
            Libro? LibroActual = (from e in _libroContext.libro
                                  where e.Id == id
                                  select e).FirstOrDefault();
            if (LibroActual == null)
            {
                return NotFound();
            }
            LibroActual.Titulo = LibroModificar.Titulo;
            LibroActual.AñoPublicacion = LibroModificar.AñoPublicacion;
            LibroActual.AutorId = LibroModificar.AutorId;
            LibroActual.CategoriaId = LibroModificar.CategoriaId;
            LibroActual.Resumen = LibroModificar.Resumen;

            _libroContext.Entry(LibroActual).State = EntityState.Modified;
            _libroContext.SaveChanges();
            return Ok(LibroModificar);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            Libro? Libro = (from e in _libroContext.libro
                            where e.Id == id
                            select e).FirstOrDefault();
            if (Libro == null)
            {
                return NotFound();

            }
            _libroContext.libro.Attach(Libro);
            _libroContext.libro.Remove(Libro);
            _libroContext.SaveChanges();

            return Ok(Libro);
        }
    }

}
