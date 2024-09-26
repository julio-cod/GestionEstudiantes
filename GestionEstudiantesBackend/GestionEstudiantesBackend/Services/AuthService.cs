using GestionEstudiantesBackend.Data.DBContext;
using GestionEstudiantesBackend.Data.Interfaces;
using GestionEstudiantesBackend.Helpers;
using GestionEstudiantesBackend.Models.DTOs;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using GestionEstudiantesBackend.Models.Entities;

namespace GestionEstudiantesBackend.Services
{
    public class AuthService : IAuthService
    {
        private GestionEstudianteDbContext _gestionEstudianteDbContext;
        public string secret = "4zUjeusAJUl7UECPtFwFNF9KapVnKtubuSN2O4q4QWI";
        public AuthService(GestionEstudianteDbContext gestionEstudianteDbContext)
        {
            _gestionEstudianteDbContext = gestionEstudianteDbContext;
        }

        public async Task<ResponseRequest> Login(AuthenticateRequest user)
        {
            // Instancia para respuestas exitosas
            ResponseRequest resp = new ResponseRequest();


            try
            {

                var userExists = await _gestionEstudianteDbContext.Usuarios.FirstOrDefaultAsync(exist => exist.Usuario == user.Usuario && exist.Clave == user.Clave);



                if (userExists == null)
                {

                    resp.Operation = false;
                    resp.Message = "Usuario no registrado";
                    resp.Data = Array.Empty<string>();
                    return resp;


                }


                var token = await generateJwtToken(userExists);


                var authenticatedUser = new AuthenticateResponse(userExists, token);

                resp.Operation = true;
                resp.Message = "Usuario logueado exitosamente";
                resp.Data = authenticatedUser;
                return resp;

            }
            catch (Exception ex)
            {
                resp.Operation = false;
                resp.Message = "El usuario no pudo ser logueado";
                resp.Data = Array.Empty<string>();
                return resp;
            }
        }

        private async Task<string> generateJwtToken(UsuariosModel user)
        {
            try
            {
                // generate token that is valid for 7 days
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.IdUsuario.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AuthenticateResponse> GetCurrentUser(string token)
        {
            try
            {
                if (token == null) { return null; }

                var tokenHandler = new JwtSecurityTokenHandler();
                //var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    //IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                var user = await _gestionEstudianteDbContext.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == userId);


                //Save log
                return new AuthenticateResponse(user, token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
