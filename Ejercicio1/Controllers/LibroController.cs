using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ejercicio1.Models;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly biblioContext _libroContext;
        public LibroController(biblioContext libroContext)
        {
            _libroContext = libroContext;
        }
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Libro> listadoLibro = (from e in _libroContext.Libro select e).ToList();

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
            Libro? equipo = (from e in _libroContext.Libro where e.Id == id select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FindByDescription(string filtro)
        {
            Libro? libro = (from e in _libroContext.Libro where e.Resumen.Contains(filtro) select e).FirstOrDefault();

            if (libro == null)
            {
                return NotFound();
            }
            return Ok(libro);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarEquipo([FromBody] Libro libro)
        {
            try
            {
                _libroContext.Libro.Add(libro);
                _libroContext.SaveChanges();
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarEquipo(int id, [FromBody] Libro libroModificar)
        {
            Libro? equipoActual = (from e in _libroContext.Libro where e.Id == id select e).FirstOrDefault();

            if (equipoActual == null)
            { return NotFound(); }

            equipoActual.Titulo = libroModificar.Titulo;
            equipoActual.descripcion = libroModificar.descripcion;
            equipoActual.marca_id = libroModificar.marca_id;
            equipoActual.tipo_equipo_id = libroModificar.tipo_equipo_id;
            equipoActual.anio_compra = libroModificar.anio_compra;
            equipoActual.costo = libroModificar.costo;

            _equiposContext.Entry(equipoActual).State = EntityState.Modified;
            _equiposContext.SaveChanges();

            return Ok(libroModificar);
        }
    }
}
