using Api_Usuario.Context;
using Api_Usuario.DTO;
using Api_Usuario.Modelo;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
//using Newtonsoft.Json;
using System.Text.Json;
namespace Api_Usuario.Servicios
{
    public class ServicioUsuario : IUsuario
    {
        private readonly UsuarioContext _usuarioContext;

        public ServicioUsuario(UsuarioContext contexto)
        {
            _usuarioContext = contexto;
        }



        public async Task<Respuesta<string>> AgregarUsuario(Usuario user)
        {

            // hacemos la modificacion para que la contraseña que se guarden de ahora en adelante se guarde con el hash en la base de datos
            var respuesta = new Respuesta<string>();

            try


            {
                //aqui validamos en Usuarios para ver si al menos uno de esos usuarios tiene un correo igual al de user
                var existe = await _usuarioContext.Usuarios
                .AnyAsync(u => u.Correo == user.Correo);

                if (existe)
                {
                    respuesta.Successful = false;
                    respuesta.Message = "No pueden haber usuarios con correos duplicados";
                    return respuesta;
                }

                // aqui encriptamos la contraseña
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _usuarioContext.Usuarios.Add(user);
                await _usuarioContext.SaveChangesAsync();

                try
                {
                    ServicesLog.GuardarUsuario(user);

                }
                catch (Exception ex)
                {
                    throw ex;
                }


                respuesta.Successful = true;
                respuesta.Message = "Se guardó exitosamente la persona";

                return respuesta;
            }
            catch (Exception)
            {
                respuesta.Successful = false;
                respuesta.Message = "No se guardó exitosamente la persona";
                return respuesta;
            }
        }

        public async Task<Respuesta<Usuario>> BuscarUsuarioPorId(int ID)
        {
            var respuesta = new Respuesta<Usuario>();

            try
            {
                var res = await _usuarioContext.Usuarios.FirstOrDefaultAsync(p => p.Id == ID);
                respuesta.SingleData = res;
                respuesta.Successful = true;
                return respuesta;
            }
            catch (Exception ex)
            {

                respuesta.Successful = false;
                respuesta.Errors.Add(ex.Message);
                return respuesta;
            }


        }

        //metodo para ver todos los usuarios
        public async Task<Respuesta<List<Usuario>>> VerTodosLosUsuarios()
        {

            var respuesta = new Respuesta<List<Usuario>>();
            try
            {
                var res = await _usuarioContext.Usuarios.ToListAsync();
                respuesta.SingleData = res;
                return respuesta;


            }
            catch (Exception ex)
            {
                respuesta.Successful = false;
                respuesta.Errors.Add(ex.Message);
                return respuesta;
            }
        }

        //Metodo que actualiza a los usuarios
        public async Task<Respuesta<string>> ActualizarUsuario(int ID, Usuario user)
        {
            var respuesta = new Respuesta<string>();
            try
            {
                var valid = await _usuarioContext.Usuarios.AnyAsync(p => p.Id == ID);

                if (valid)
                {
                    _usuarioContext.Usuarios.Update(user);
                    await _usuarioContext.SaveChangesAsync();
                    respuesta.Successful = true;
                    respuesta.Message = "Se guardó exitosamente la persona";
                    return respuesta;

                }
                else
                {
                    respuesta.Successful = false;
                    respuesta.Message = "El usuario que intenta modificar no existe";
                    return respuesta;
                }


            }
            catch (Exception ex)
            {

                respuesta.Successful = false;
                respuesta.Message = $"No se guardó exitosamente la persona {ex}";
                return respuesta;
            }
        }

        //Metodo que elimina a los usuarios
        public async Task<Respuesta<string>> EliminarUsuario(int ID)
        {

            var respuesta = new Respuesta<string>();
            {
                try
                {
                    var valid = _usuarioContext.Usuarios.FirstOrDefault(p => p.Id == ID);

                    if (valid != null)
                    {

                        _usuarioContext.Usuarios.Remove(valid);
                        await _usuarioContext.SaveChangesAsync();
                        respuesta.Successful = true;
                        respuesta.Message = $"Se elimino correctamente el usuario {valid?.Nombre}";

                        return respuesta;
                    }
                    else
                    {
                        respuesta.Successful = false;
                        respuesta.Message = "El usuario que intentaste eliminar no existe";
                        return respuesta;
                    }
                }
                catch
                {
                    respuesta.Successful = false;
                    respuesta.Message = "El usuario no se pudo eliminar";
                    return respuesta;
                }
            }

        }

        public async Task<Respuesta<UsuarioLog>> LeerLogUsuarios()
        {
            var respuesta = new Respuesta<UsuarioLog>();

            try
            {
                string rutaArchivo = Path.Combine("Logs", "usuario.txt");

                if (!File.Exists(rutaArchivo))
                {
                    respuesta.Successful = false;
                    respuesta.Message = "El archivo no existe";
                    return respuesta;
                }

                var jsonString = await File.ReadAllTextAsync(rutaArchivo);

                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    respuesta.Successful = true;
                    respuesta.DataList = new List<UsuarioLog>();
                    return respuesta;
                }

                var datosUsuario = JsonSerializer.Deserialize<List<UsuarioLog>>(jsonString);

                respuesta.DataList = datosUsuario ?? new List<UsuarioLog>();
                respuesta.Successful = true;

                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Successful = false;
                respuesta.Message = $"Error leyendo logs: {ex.Message}";
                return respuesta;
            }
        }



    }
    }
