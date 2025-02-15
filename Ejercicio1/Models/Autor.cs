using System.ComponentModel.DataAnnotations;

namespace Ejercicio1.Models
{
    public class Autor
    {
        [Key]
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? nacionalidad { get; set; }

    }
}
