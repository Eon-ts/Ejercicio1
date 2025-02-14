using System.ComponentModel.DataAnnotations;

namespace Ejercicio1.Models
{
    public class Autor
    {
        [Key]
        public int id { get; set; }
        public string? Nombre { get; set; }
        public string? Nacionalidad { get; set; }

    }
}
