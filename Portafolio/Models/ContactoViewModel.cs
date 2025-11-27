using System.ComponentModel.DataAnnotations;

namespace Portafolio.Models
{
    public class ContactoViewModel
    {
        public int idContacto { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El email es obligatorio")]
        public string email { get; set; }
        [Required(ErrorMessage = "El mensaje es obligatorio")]
        public string mensaje { get; set; }
    }
}
