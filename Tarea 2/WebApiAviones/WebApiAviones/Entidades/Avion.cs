using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAviones.Entidades
{
    public class Avion
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo [0] es requerido")]
        [StringLength(maximumLength: 10, ErrorMessage = "El campo de int [0] puede tener hasta 10 valores")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo [0] es requerido")]
        [NotMapped]

        public string Color { get; set; }

    }
}
