using Api_Usuario.DTO;
using Api_Usuario.Modelo;
using Api_Usuario.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Usuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _Iusuario;

        public UsuarioController(IUsuario _user)

        {
            _Iusuario = _user;
        }
        
        
        [HttpPost("/agregarusuario")]
        public async Task<ActionResult<Respuesta<string>>>
           AgregarUsuario(Usuario user) =>  await _Iusuario.AgregarUsuario(user);


        [HttpGet("/buscarporId/{ID}")]

        public async Task<ActionResult<Respuesta<Usuario>>>
            
            BuscarUsuarioPorId(int ID) =>
            await _Iusuario.BuscarUsuarioPorId(ID);

        [HttpGet("/VerTodosLosUsuarios")]

        public async Task<ActionResult<Respuesta<List<Usuario>>>>
            VerTodosLosUsuarios() => await _Iusuario.VerTodosLosUsuarios();


        [HttpPut("Actualizar/{id}")]

        public async Task<ActionResult<Respuesta<string>>>

            ActualizarUsuario(int id, Usuario user) => await _Iusuario.ActualizarUsuario(id, user);

        [HttpDelete("/EliminarUsuario")]

        public async Task<ActionResult<Respuesta<string>>>
            
            EliminarUsuario(int ID) => await _Iusuario.EliminarUsuario(ID); 

    }


}

