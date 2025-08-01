using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class TPaciente
{
    public int IdPaciente { get; set; }

    public string? Nombres { get; set; }

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public DateTime? FechaNacimiento { get; set; }
}
