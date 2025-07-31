using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contactos_moviles.Models;

public partial class Contacto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del contacto es obligatorio.")]
    [StringLength(50)]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El apellido del contacto es obligatorio.")]
    [StringLength(50)]
    public string Apellido { get; set; }

    [Required(ErrorMessage = "El teléfono del contacto es obligatorio.")]
    [Phone(ErrorMessage = "Debe ser un número de teléfono válido.")]
    public string Telefono { get; set; }

    [Required(ErrorMessage = "El correo del contacto es obligatorio.")]
    [EmailAddress(ErrorMessage = "Debe ser un correo electrónico válido.")]
    public string Correo { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "La dirección un campo obligatorio")]
    public string Direccion { get; set; } = "";


}



