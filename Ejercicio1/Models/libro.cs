using System.ComponentModel.DataAnnotations;
namespace Ejercicio1.Models
{
    public class libro
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int AñoPublicacion { get; set; }
        public int AutorId { get; set; }
        public int CategoriaId { get; set; }
        public string? Resumen { get; set; }

    }
}
