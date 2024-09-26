using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using GestionEstudiantesBackend.Models.Entities;

namespace GestionEstudiantesBackend.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (UsuariosModel?)context.HttpContext.Items["Usuario"];

            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            return;
        }


    }
}
