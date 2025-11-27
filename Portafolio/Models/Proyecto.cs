using System.ComponentModel.DataAnnotations;

namespace Portafolio.Models
{
    public class Proyecto
    {
        public int idProyecto { get; set; }
        [Required (ErrorMessage = "El título es obligatorio")]
        public string Titulo { get; set; }
        [Required (ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; }
        [Required (ErrorMessage = "La imagen es obligatoria")]
        public string ImagenURL { get; set; }
        [Required (ErrorMessage = "El link es obligatorio")]
        public string Link { get; set; }
        public int orden {  get; set; }
    }
}
