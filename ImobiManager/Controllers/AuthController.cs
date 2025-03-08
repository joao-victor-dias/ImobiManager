using ImobiManager.Auth;
using ImobiManager.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ImobiManager.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login request)
        {
            if (request.Username == "admin" && request.Password == "admin")  
            {
                var token = _jwtService.GenerateToken(request.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }        
    }
}
