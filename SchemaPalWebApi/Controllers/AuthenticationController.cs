using Microsoft.AspNetCore.Mvc;
using SchemaPalWebApi.DataTransferObjects;
using SchemaPalWebApi.Models;
using SchemaPalWebApi.Repositories;
using SchemaPalWebApi.Services;
using System.Net;
using System.Security.Claims;

namespace SchemaPalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordService _passwordService;

        public AuthenticationController(
            IUserRepository userRepository, 
            ITokenService tokenService, 
            IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordService = passwordService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegistration userRegistration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_userRepository.UserExists(userRegistration.Username))
            {
                return Conflict("Korisničko ime već postoji.");
            }

            var passwordHash = _passwordService.HashPassword(userRegistration.Password);

            var user = new UserRecord
            {
                Username = userRegistration.Username,
                PasswordHash = passwordHash
            };

            _userRepository.CreateUser(user);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUserByUsername(userLogin.Username);
            if (user == null || !_passwordService.VerifyPassword(userLogin.Password, user.PasswordHash))
            {
                return Unauthorized();
            }

            var token = _tokenService.GenerateToken(user.Id);

            return Ok(new { Token = token });
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshToken refreshToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(refreshToken.Token);
            if (principal == null)
            {
                return Unauthorized("Token nije moguće obnoviti.");
            }

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepository.GetUserById(Guid.Parse(userIdClaim));
            if (user == null)
            {
                return Unauthorized("Korisnik nije pronađen.");
            }

            var newToken = _tokenService.GenerateToken(user.Id);

            return Ok(new { Token = newToken });
        }
    }
}
