using System.ComponentModel.DataAnnotations;

namespace Contactos_moviles.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string Clave { get; set; }
    }
}

