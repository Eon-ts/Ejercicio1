using System.ComponentModel.DataAnnotations;
namespace Ejercicio1.Models
{
    public class Libro
    {
        [Key]
        public int id { get; set; }
        public string titulo { get; set; }
        public int aniopublicacion { get; set; }
        public int autorid { get; set; }
        public int categoriaid { get; set; }
        public string? resumen { get; set; }

    }
}
