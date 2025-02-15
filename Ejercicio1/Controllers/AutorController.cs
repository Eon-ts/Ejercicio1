using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ejercicio1.Models;
using Microsoft.EntityFrameworkCore;
namespace Ejercicio1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly biblioContext _autorContext;
        public AutorController(biblioContext autorContexto)
        {
            _autorContext = autorContexto;
        }
        /// <summary>
        ///  Endpoint que retorna
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Autor> listadoAutor = (from e in _autorContext.Autor
                                        select e).ToList();
            if (listadoAutor.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoAutor);
        }
        /*
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            Autor? autor = (from e in _autorContext.Autor
                            where e.id == id
                            select e).FirstOrDefault();
            if (autor == null)
            {
                return NotFound();
            }
            return Ok(autor);
        }
        */
        
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            var autorlibros = (from e in _autorContext.Autor
                               join l in _autorContext.libro on e.id equals l.autorid
                               where e.id == id
                               select e).FirstOrDefault();

            if (autorlibros == null)
            {
                return NotFound();
            }

            var libros = _autorContext.libro.Where(l => l.autorid == id).ToList();

            var resultado = new
            {
                autor = autorlibros,
                libros = libros
            };

            return Ok(resultado);
        }
        


        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody] Autor autor)
        {
            try
            {
                _autorContext.Autor.Add(autor);
                _autorContext.SaveChanges();
                return Ok(autor);
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
        public IActionResult ActualizarEquipo(int id, [FromBody] Autor autorModificar)
        {
            Autor? autorActual = (from e in _autorContext.Autor
                                  where e.id == id
                                  select e).FirstOrDefault();
            if (autorActual == null)
            {
                return NotFound();
            }
            autorActual.nombre = autorModificar.nombre;
            autorActual.nacionalidad = autorModificar.nacionalidad;

            _autorContext.Entry(autorActual).State = EntityState.Modified;
            _autorContext.SaveChanges();
            return Ok(autorModificar);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            Autor? autor = (from e in _autorContext.Autor
                            where e.id == id
                            select e).FirstOrDefault();
            if (autor == null)
            {
                return NotFound();

            }
            _autorContext.Autor.Attach(autor);
            _autorContext.Autor.Remove(autor);
            _autorContext.SaveChanges();

            return Ok(autor);
        }
    }
}
