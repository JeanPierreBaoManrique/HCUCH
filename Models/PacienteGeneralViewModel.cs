namespace WebApp.Models
{
    public class PacienteGeneralViewModel
    {
        public int IdPaciente { get; set; }
        public string? Nombres { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? InformacionGeneral { get; set; }
    }
}
