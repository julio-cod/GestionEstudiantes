using GestionEstudiantesBackend.Data.Interfaces;
using GestionEstudiantesBackend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestionEstudiantesBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : Controller
    {
        private readonly ICursoRepository _cursoRepository;
        public CursosController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        [HttpGet]
        //[Authorize]
        [Route("ListaDeCursos")]
        public async Task<IActionResult> ListaDeCursos()
        {
            ResponseRequest resp = new ResponseRequest();
            try
            {

                resp = await _cursoRepository.ListaDeCursos();

                if (resp.Operation != true)
                {
                    return BadRequest(resp);
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                resp.Operation = false;
                resp.Message = "Error al tratar de obtener lista de cursos" + ex.Message;
                resp.Data = Array.Empty<string>();
                return BadRequest(resp);
            }
        }
    }
}
