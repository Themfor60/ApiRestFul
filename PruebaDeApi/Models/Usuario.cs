using System.ComponentModel.DataAnnotations;

namespace PruebaDeApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligado")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido es obligado")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El Telefono es obligado")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "La direccion es obligado")]
        public string direccion { get; set; }
    }
}
