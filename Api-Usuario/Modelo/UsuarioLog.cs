using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace Api_Usuario.Modelo
{
    public class UsuarioLog
    {

        

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }
  
        public DateTime FechaDeNacimiento { get; set; }

        public string evento {  get; set; }
        public DateTime Fecha {  get; set; }


    }
}
