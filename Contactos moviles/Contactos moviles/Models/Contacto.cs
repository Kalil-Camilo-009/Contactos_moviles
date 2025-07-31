using System;
using System.Collections.Generic;

namespace Contactos_moviles.Models;

public partial class Contacto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public DateTime Fecha { get; set; }
}
