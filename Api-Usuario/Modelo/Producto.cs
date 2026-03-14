    using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Usuario.Modelo
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        
        
        [Required]
        public string NombreProducto{get;set;}

        [Required]
       
        public decimal Precio {  get; set;}

        

        //claves foraneas hacen referencia a proveedor y categoria
        public int IdProveedor{ get; set; }

        [ForeignKey("IdProveedor")]
        public Proveedor Proveedor { get; set; }


        public int IdCategoria {  get; set; }

        [ForeignKey("IdCategoria")]
        public Categoría Categoría { get; set; }
    }
}
