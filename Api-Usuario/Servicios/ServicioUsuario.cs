using Api_Usuario.Context;
using Api_Usuario.DTO;
using Api_Usuario.Modelo;
using Microsoft.EntityFrameworkCore;

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

                _usuarioContext.Usuarios.Add(user);
                await _usuarioContext.SaveChangesAsync();

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

        public async Task<Respuesta<Usuario>>BuscarUsuarioPorId(int ID)
        {
            var respuesta = new Respuesta<Usuario>();
   
            try
            {
                var res = await _usuarioContext.Usuarios.FirstOrDefaultAsync(p => p.Id == ID);
                respuesta.SingleData = res;
                respuesta.Successful = true;
                return respuesta;
            }
            catch (Exception ex) {

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
            catch (Exception ex) {
                respuesta.Successful = false;
                respuesta.Errors.Add(ex.Message);
                return respuesta;
            }
        }

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
            catch (Exception ex) {
            
                respuesta.Successful = false;
                respuesta.Message = $"No se guardó exitosamente la persona {ex}";
                return respuesta;
            }
        }

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
    }
}
