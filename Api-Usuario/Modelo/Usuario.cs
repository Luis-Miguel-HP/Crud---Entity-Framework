using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Usuario.Modelo
{
    public class Usuario
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaDeNacimiento { get; set; }

    }
}
