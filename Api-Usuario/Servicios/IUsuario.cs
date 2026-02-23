using Api_Usuario.DTO;
using Api_Usuario.Modelo;
using Microsoft.AspNetCore.Mvc;

namespace Api_Usuario.Servicios
{
    public interface IUsuario
    {

        Task<Respuesta<List<Usuario>>> VerTodosLosUsuarios(); 
        Task<Respuesta<Usuario>> BuscarUsuarioPorId(int ID);
        Task<Respuesta<string>> AgregarUsuario(Usuario user);

        Task<Respuesta<string>> ActualizarUsuario(int ID, Usuario user);

        Task<Respuesta<string>> EliminarUsuario(int id);
   

    }
}
