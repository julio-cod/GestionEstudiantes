using GestionEstudiantesBackend.Data.Interfaces;
using GestionEstudiantesBackend.Helpers;
using GestionEstudiantesBackend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestionEstudiantesBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : Controller
    {
        private readonly IEstudianteRepository _estudianteRepository;
        public EstudiantesController(IEstudianteRepository estudianteRepository)
        {
            _estudianteRepository = estudianteRepository;
        }

        [HttpPost]
        [Authorize]
        [Route("RegistrarEstudiante")]
        public async Task<IActionResult> RegistrarEstudiante(RegistrarEstudianteDto registrarEstudianteDto)
        {
            ResponseRequest resp = new ResponseRequest();
            try
            {
                
                resp = await _estudianteRepository.RegistrarEstudiante(registrarEstudianteDto);

                if (resp.Operation != true)
                {
                    return BadRequest(resp);
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                resp.Operation = false;
                resp.Message = "Error al intentar agregar Estudiante" + ex.Message;
                resp.Data = Array.Empty<string>();
                return BadRequest(resp);
            }

            
        }

        [HttpPost]
        [Authorize]
        [Route("EditarEstudiante")]
        public async Task<IActionResult> EditarEstudiante(EditarEstudianteDto editarEstudianteDto)
        {
            ResponseRequest resp = new ResponseRequest();
            try
            {

                resp = await _estudianteRepository.EditarEstudiante(editarEstudianteDto);

                if (resp.Operation != true)
                {
                    return BadRequest(resp);
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                resp.Operation = false;
                resp.Message = "Error al modificar Estudiante" + ex.Message;
                resp.Data = Array.Empty<string>();
                return BadRequest(resp);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("EliminarEstudiante")]
        public async Task<IActionResult> EliminarEstudiante(EliminarEstudianteDto eliminarEstudianteDto)
        {
            ResponseRequest resp = new ResponseRequest();
            try
            {

                resp = await _estudianteRepository.EliminarEstudiante(eliminarEstudianteDto);

                if (resp.Operation != true)
                {
                    return BadRequest(resp);
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                resp.Operation = false;
                resp.Message = "Error al eliminar Estudiante" + ex.Message;
                resp.Data = Array.Empty<string>();
                return BadRequest(resp);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("ListaDeEstudiantes")]
        public async Task<IActionResult> ListaDeEstudiantes()
        {
            ResponseRequest resp = new ResponseRequest();
            try
            {

                resp = await _estudianteRepository.ListaDeEstudiantes();

                if (resp.Operation != true)
                {
                    return BadRequest(resp);
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                resp.Operation = false;
                resp.Message = "Error al tratar de obtener lista de estudiantes" + ex.Message;
                resp.Data = Array.Empty<string>();
                return BadRequest(resp);
            }
        }

    }
}
