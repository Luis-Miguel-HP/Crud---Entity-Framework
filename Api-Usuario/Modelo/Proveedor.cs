
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Api_Usuario.Modelo
{
    public class Proveedor
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        public string NombreProvedor { get; set; }


        [Required]
        [MinLength(8)]
        [MaxLength(8)]
        public int Conctacto { get; set; }

    }
}
