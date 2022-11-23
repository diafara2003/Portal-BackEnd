using System.ComponentModel.DataAnnotations;

namespace Code.Repository.Model.DTO.Login
{
    public class UserRegistrationDto
    {
        public string tipoDocumento { get; set; }

        [Required(ErrorMessage = "Correo obligatorio")]
        public string correo { get; set; }

        [Required(ErrorMessage = "NIT obligatorio")]
        public string documento { get; set; }

        public int? codigoVerificacion { get; set; }

        [Required(ErrorMessage = "El nombre de la empresa es obligatorio")]
        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El tipo de persona es obligatorio")]
        public string TipoPersona { get; set; }

        public bool IsVinculoConstructora { get; set; }

    }
}
