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


        [HttpGet("buscarporId")]

        public async Task<ActionResult<Respuesta<Usuario>>>
            
            BuscarUsuarioPorId(int ID) =>
            await _Iusuario.BuscarUsuarioPorId(ID);


    }


}

