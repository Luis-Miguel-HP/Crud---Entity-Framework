using Api_Usuario.DTO;
using Api_Usuario.Modelo;
using Api_Usuario.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api_Usuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _Iusuario;
        private readonly IAutenticacion _autenticacion;

        public UsuarioController(IUsuario _user, IAutenticacion _Aut)

        {
            _Iusuario = _user;
            _autenticacion = _Aut;
        }


        [HttpPost("/agregarusuario")]
        public async Task<ActionResult<Respuesta<string>>>
           AgregarUsuario(Usuario user) => await _Iusuario.AgregarUsuario(user);


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
        
        [Authorize]
        [HttpDelete("EliminarUsuario/{id}")]

        public async Task<ActionResult<Respuesta<string>>>

            EliminarUsuario(int ID) => await _Iusuario.EliminarUsuario(ID);

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _autenticacion.Autenticacion(request.Nombre, request.Password);

            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [HttpPost("refrescartoken")]
        public IActionResult Refresh([FromBody] string token) {
            var ServicioToken = new TokenServices();

            var principal = ServicioToken.LeerTokenExpirado(token);

            var NombreUsuario = principal.FindFirst(ClaimTypes.Name)?.Value;

            var id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var newToken = ServicioToken.GeneradorToken(NombreUsuario, id);

            return Ok(newToken);


        }
    }

}

