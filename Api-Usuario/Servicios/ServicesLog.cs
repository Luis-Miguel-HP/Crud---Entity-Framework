using Api_Usuario.Modelo;
using System.Text.Json;

namespace Api_Usuario.Servicios
{
    public class ServicesLog
    {
   
        public static void GuardarUsuario(Usuario usuario)
        {
            string carpeta = "Logs";
            string rutaArchivo = Path.Combine(carpeta, "usuario.txt");

            try
            {
                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                var log = new UsuarioLog
                {
                    evento = "Usuario_Creado",
                    Fecha = DateTime.Now,
                    Nombre = usuario.Nombre,
                    Correo = usuario.Correo,
                    FechaDeNacimiento = usuario.FechaDeNacimiento,
                    Id = usuario.Id
                };

                List<UsuarioLog> lista = new List<UsuarioLog>();

                if (File.Exists(rutaArchivo))
                {
                    var contenido = File.ReadAllText(rutaArchivo);

                    if (!string.IsNullOrWhiteSpace(contenido))
                    {
                        lista = JsonSerializer.Deserialize<List<UsuarioLog>>(contenido)
                                ?? new List<UsuarioLog>();
                    }
                }

                lista.Add(log);

                var opciones = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                var json = JsonSerializer.Serialize(lista, opciones);

                File.WriteAllText(rutaArchivo, json);
            }
            catch (Exception ex)
            {
           
                Console.WriteLine($"Error guardando log: {ex.Message}");
            }
        }
    }
}
