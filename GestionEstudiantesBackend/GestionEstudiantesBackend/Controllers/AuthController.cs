using GestionEstudiantesBackend.Data.Interfaces;
using GestionEstudiantesBackend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestionEstudiantesBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(AuthenticateRequest user)
        {
           
            try
            {
                var resp = await _authService.Login(user);

                if (resp.Operation != true)
                {
                    return BadRequest(resp);
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                // Instancia para respuestas
                ResponseRequest resp = new ResponseRequest();
                resp.Operation = false;
                resp.Message = "Error al intentar acceder - " + ex.Message;
                resp.Data = Array.Empty<string>();
                return BadRequest(resp);
            }
        }

        [HttpGet]
        [Route("GetCurrentUser/{token}")]
        public async Task<IActionResult> GetCurrentUser(string token)
        {
            // Instancia para respuestas
            ResponseRequest resp = new ResponseRequest();

            try
            {
                var usuario = await _authService.GetCurrentUser(token);

                if (usuario == null)
                {
                    resp.Operation = false;
                    resp.Message = "No se pudo obetener el usuario";
                    resp.Data = Array.Empty<string>();
                    return BadRequest(resp);
                }
                resp.Operation = true;
                resp.Message = "Usuario obtenido exitosamente";
                resp.Data = usuario;
                return Ok(resp);
            }
            catch (Exception ex)
            {
                resp.Operation = false;
                resp.Message = "Error al intentar obtener el usuario actual - " + ex.Message;
                resp.Data = Array.Empty<string>();
                return BadRequest(resp);
            }
        }
    }
}
