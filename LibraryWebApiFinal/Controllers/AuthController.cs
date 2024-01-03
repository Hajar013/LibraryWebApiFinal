using BLL.DTOs;
using BLL.Services.AuthServices;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryWebApiFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
         
        [HttpPost("Register")]
        public IActionResult Register([FromBody] PersonDto person)
        {
            // Validate librarian DTO or handle validation errors
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _authService.Register(person);
                var token = _authService.GenerateJwtToken(person);
                return StatusCode(200, new { Token = token });

            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your requirement
                return StatusCode(500);
            }
        }
         
        [HttpPost("login")]
        public IActionResult Login([FromBody] PersonDto personDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var person = _authService.Authenticate(personDto.UserName, personDto.Password);

                if (person == null)
                    return Unauthorized("Invalid username or password");

                var token = _authService.GenerateJwtToken(person);
                return StatusCode(200, new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


    }
}
