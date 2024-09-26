using GestionEstudiantesBackend.Data.DBContext;
using GestionEstudiantesBackend.Data.Interfaces;
using GestionEstudiantesBackend.Models.DTOs;
using GestionEstudiantesBackend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionEstudiantesBackend.Data.Repositories
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private GestionEstudianteDbContext _gestionEstudianteDbContext;
        public EstudianteRepository(GestionEstudianteDbContext gestionEstudianteDbContext) 
        {
            _gestionEstudianteDbContext = gestionEstudianteDbContext;
        }

        public async Task<ResponseRequest> RegistrarEstudiante(RegistrarEstudianteDto registrarEstudianteDto)
        {
            //Instancia para respuestas
            ResponseRequest resp = new ResponseRequest();
            try
            {
                EstudianteModel estudianteModel = new EstudianteModel();
                estudianteModel.Nombre= registrarEstudianteDto.Nombre;
                estudianteModel.Apellido= registrarEstudianteDto.Apellido;
                estudianteModel.Edad = registrarEstudianteDto.Edad;
                estudianteModel.FechaNacimiento = registrarEstudianteDto.FechaNacimiento;
                estudianteModel.IdCurso= registrarEstudianteDto.IdCurso;
                estudianteModel.Direccion = registrarEstudianteDto.Direccion;

                _gestionEstudianteDbContext.Estudiantes.Add(estudianteModel);
                await _gestionEstudianteDbContext.SaveChangesAsync();

                resp.Operation = true;
                resp.Message = "Estudiante resgistrado exitosamente";
                resp.Data = Array.Empty<string>();
                return resp;
            }
            catch (Exception ex)
            {

                resp.Operation = false;
                resp.Data = Array.Empty<string>();
                resp.Message = "Error al intentar registrar estudiante - " +  ex.Message.ToString();
                return resp;
            }
        }

        public async Task<ResponseRequest> EditarEstudiante(EditarEstudianteDto editarEstudianteDto)
        {
            //Instancia para respuestas
            ResponseRequest resp = new ResponseRequest();
            try
            {
                var estudianteModel = await _gestionEstudianteDbContext.Estudiantes.FirstOrDefaultAsync(p => p.IdEstudiante == editarEstudianteDto.IdEstudiante);

                estudianteModel.IdEstudiante = editarEstudianteDto.IdEstudiante;
                estudianteModel.Nombre = editarEstudianteDto.Nombre;
                estudianteModel.Apellido = editarEstudianteDto.Apellido;
                estudianteModel.Edad = editarEstudianteDto.Edad;
                estudianteModel.FechaNacimiento = editarEstudianteDto.FechaNacimiento;
                estudianteModel.IdCurso = editarEstudianteDto.IdCurso;
                estudianteModel.Direccion = editarEstudianteDto.Direccion;

                _gestionEstudianteDbContext.Entry(estudianteModel).State = EntityState.Modified;
                await _gestionEstudianteDbContext.SaveChangesAsync();

                resp.Operation = true;
                resp.Message = "Estudiante actualizado exitosamente";
                resp.Data = Array.Empty<string>();
                return resp;
            }
            catch (Exception ex)
            {

                resp.Operation = false;
                resp.Data = Array.Empty<string>();
                resp.Message = "Error al intentar modificar estudiante - " + ex.Message.ToString();
                return resp;
            }
        }

        public async Task<ResponseRequest> EliminarEstudiante(EliminarEstudianteDto eliminarEstudianteDto)
        {
            // Instancia para respuestas
            ResponseRequest resp = new ResponseRequest();


            try
            {

                var estudiante = _gestionEstudianteDbContext.Estudiantes.FirstOrDefault(p => p.IdEstudiante == eliminarEstudianteDto.IdEstudiante);

                if (estudiante != null)
                {
                    _gestionEstudianteDbContext.Estudiantes.Remove(estudiante);
                    await _gestionEstudianteDbContext.SaveChangesAsync();
                }
                else
                {
                    resp.Operation = false;
                    resp.Message = "No se encuentra el estudiante: " + eliminarEstudianteDto.IdEstudiante.ToString();
                    resp.Data = Array.Empty<string>();
                    return resp;
                }


                resp.Operation = true;
                resp.Message = "Estudiante eliminado exitosamente";
                resp.Data = Array.Empty<string>();
                return resp;

            }
            catch (Exception ex)
            {
                resp.Operation = false;
                resp.Message = "Error al intentar eliminar Estudiante - " + ex.Message;
                resp.Data = Array.Empty<string>();
                return resp;
            }

        }

        public async Task<ResponseRequest> ListaDeEstudiantes()
        {
            // Instancia para respuestas
            ResponseRequest resp = new ResponseRequest();


            try
            {
                var estudiantes = await _gestionEstudianteDbContext.Estudiantes.ToListAsync();

                if (estudiantes.Count() == 0)
                {
                    resp.Operation = false;
                    resp.Message = "No se encontraron estudiantes registrados";
                    resp.Data = Array.Empty<string>();
                    return resp;
                }

                List<ListaEstudiantesDto> listaEstudiantesDtoList = new List<ListaEstudiantesDto>();
                foreach (var item in estudiantes)
                {
                    ListaEstudiantesDto estudianteModel = new ListaEstudiantesDto();
                    estudianteModel.IdEstudiante = item.IdEstudiante;
                    estudianteModel.Nombre = item.Nombre;
                    estudianteModel.Apellido = item.Apellido;
                    estudianteModel.Edad = item.Edad;
                    estudianteModel.FechaNacimiento = item.FechaNacimiento;
                    estudianteModel.IdCurso = item.IdCurso;
                    estudianteModel.Direccion = item.Direccion;

                    listaEstudiantesDtoList.Add(estudianteModel);
                }

                resp.Operation = true;
                resp.Message = "Busqueda exitosa";
                resp.Data = listaEstudiantesDtoList;
                return resp;

            }
            catch (Exception ex)
            {
                resp.Operation = false;
                resp.Message = "Error al intentar obtener listas de estudiantes - " + ex.Message;
                resp.Data = Array.Empty<string>();
                return resp;
            }

        }

    }
}
