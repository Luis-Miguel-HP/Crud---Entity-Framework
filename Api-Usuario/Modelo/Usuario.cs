using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Usuario.Modelo
{
    public class Usuario
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Este campo es requerido")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(50)]
        [EmailAddress]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Date)]
        public DateTime FechaDeNacimiento { get; set; }

        [Required]
        [MinLength(8)]
        public string Password {  get; set; }

    }
}
