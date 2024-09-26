using GestionEstudiantesBackend.Data.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace GestionEstudiantesBackend.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public string secret = "4zUjeusAJUl7UECPtFwFNF9KapVnKtubuSN2O4q4QWI";

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUsuarioRepository usuarioRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                attachUserToContext(context, usuarioRepository, token);
            }

            await _next(context);
        }

        private async void attachUserToContext(HttpContext context, IUsuarioRepository usuarioRepository, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    RequireExpirationTime = false,
                    //LifetimeValidator = TokenLifetimeValidator.Validate
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    //ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = long.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                //attach user to context on successful jwt validation

                context.Items["Usuario"] = await usuarioRepository.GetUserById(userId);
            }
            catch (Exception)
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
                throw;
            }
        }

        public static class TokenLifetimeValidator
        {
            //Proceso para que el token no expire
            public static bool Validate(
                DateTime? notBefore,
                DateTime? expires,
                SecurityToken tokenToValidate,
                TokenValidationParameters @param
            )
            {
                //return (expires != null && expires > DateTime.UtcNow || expires < DateTime.UtcNow);
                return (expires != null && expires > DateTime.UtcNow || expires < DateTime.UtcNow);
            }
        }
    }
}
