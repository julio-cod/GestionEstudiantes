using GestionEstudiantesBackend.Data.DBContext;
using GestionEstudiantesBackend.Data.Interfaces;
using GestionEstudiantesBackend.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GestionEstudiantesBackend.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private GestionEstudianteDbContext _gestionEstudianteDbContext;
        public CursoRepository(GestionEstudianteDbContext gestionEstudianteDbContext)
        {
            _gestionEstudianteDbContext = gestionEstudianteDbContext;
        }

        public async Task<ResponseRequest> ListaDeCursos()
        {
            // Instancia para respuestas
            ResponseRequest resp = new ResponseRequest();


            try
            {
                var cursos = await _gestionEstudianteDbContext.Cursos.ToListAsync();

                if (cursos.Count() == 0)
                {
                    resp.Operation = false;
                    resp.Message = "No se encontraron cursos registrados";
                    resp.Data = Array.Empty<string>();
                    return resp;
                }

                List<ListoCursosDto> ListoCursosDtoList = new List<ListoCursosDto>();
                foreach (var item in cursos)
                {
                    ListoCursosDto listoCursosDto = new ListoCursosDto();
                    listoCursosDto.IdCurso = item.IdCurso;
                    listoCursosDto.Descripcion = item.Descripcion;

                    ListoCursosDtoList.Add(listoCursosDto);
                }

                resp.Operation = true;
                resp.Message = "Busqueda exitosa";
                resp.Data = ListoCursosDtoList;
                return resp;

            }
            catch (Exception ex)
            {
                resp.Operation = false;
                resp.Message = "Error al intentar obtener listas de cursos - " + ex.Message;
                resp.Data = Array.Empty<string>();
                return resp;
            }

        }

    }
}
