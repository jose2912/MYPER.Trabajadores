using System.ComponentModel.DataAnnotations;

namespace MYPER.Trabajadores.Entity
{
    public class Trabajador
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? Nombres { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? Apellidos { get; set; }

        [Required(ErrorMessage = "El tipo de documento es obligatorio")]
        [StringLength(20)]
        public string? TipoDocumento { get; set; }

        [Required(ErrorMessage = "El número de documento es obligatorio")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "El DNI debe tener exactamente 8 dígitos")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El DNI debe contener solo números")]
        public string? NumeroDocumento { get; set; }


        [Required(ErrorMessage = "El sexo es obligatorio")]
        [StringLength(10)]
        public string? Sexo { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }
        public bool Estado { get; set; } = true;
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string? Direccion { get; set; }

        public string? FotoRuta { get; set; }
    }
}

