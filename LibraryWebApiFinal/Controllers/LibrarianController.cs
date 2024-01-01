using AutoMapper;
using BLL.DTOs;
using BLL.Services;
using BLL.Services.AuthorServices;
using BLL.Services.LibrarianServices;
using BLL.Services.PersonServices;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace LibraryWebApiFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrarianController : ControllerBase
    {

        private readonly ILibrarianService _librarianServices;
        private readonly IMapper _mapper;
        private readonly IAuthService<LibrarianDto> _authService;
        public LibrarianController(ILibrarianService librarianServices, IMapper mapper, IAuthService<LibrarianDto> authService)
        {
            _librarianServices = librarianServices;
            _mapper = mapper;
            _authService = authService;
        }
        //[Authorize]
        [Authorize(Policy = "LibrarianPolicy")]
        [HttpGet("GetLibrarians")]
        public IQueryable<LibrarianDto> Get()
        {
            var librarians = _librarianServices.FindAll();
            return librarians;
        }

        [HttpGet]
        [Route("GetLibrarian/{id}")]
        public IQueryable<LibrarianDto> GetById(int id)
        {
            var librarian = _librarianServices.FindByCondition(id);
            return librarian;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LibrarianDto librarianDto)
        {
            var librarian = _authService.Authenticate(librarianDto.Person.UserName, librarianDto.Person.Password);

            if (librarian == null)
                return Unauthorized("Invalid username or password");

            var token = _authService.GenerateJwtToken(librarian);
            return Ok(new { Token = token });
        }


        [HttpPost("Register")]
        public IActionResult Register([FromBody] LibrarianDto librarian)
        {
            // Validate librarian DTO or handle validation errors

            librarian.Person.Role = "librarian";

            try
            {
                _authService.Register(librarian);
                //_librarianServices.Create(librarian);

                // Assuming your Create method sets the Id of the created librarian, you can retrieve it
                int createdLibrarianId = librarian.Id;

                var token = _authService.GenerateJwtToken(librarian);

                return CreatedAtAction("GetById", new { id = createdLibrarianId }, new { Token = token });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your requirement
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPut]
        [Route("Edit/{id}")]
        public ActionResult<LibrarianDto> EditPerson(int id, [FromBody] LibrarianDto updatedLibrarian)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingLibrarian = _librarianServices.FindByCondition(id).FirstOrDefault();

                if (existingLibrarian == null)
                {
                    return NotFound($"Librarian with ID {id} not found");
                }

                // Update properties of existingLibrarian with values from updatedLibrarian
                existingLibrarian.LibrarianlicenseNumber = updatedLibrarian.LibrarianlicenseNumber;
              

                // Use AutoMapper to map the updated entity back to DTO if needed
                var updatedLibrarianDto = _mapper.Map<LibrarianDto>(existingLibrarian);

                // Save changes to the repository
                _librarianServices.Update(existingLibrarian);

                return Ok(updatedLibrarianDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your requirement
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult DeleteLibrarian(int id)
        {
            try
            {
                var existingLibrarian = _librarianServices.FindByCondition(id).FirstOrDefault();

                if (existingLibrarian == null)
                {
                    return NotFound($"Librarian with ID {id} not found");
                }

                // Use your service to delete the Librarian
                _librarianServices.Delete(existingLibrarian);

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your requirement
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("CheckLibrarianRole")]
        public ActionResult CheckLibrarianRole()
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Check if the user has the "librarian" role claim
                var isLibrarian = User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "librarian");

                if (isLibrarian)
                {
                    // User is a librarian
                    return Ok("User is a librarian.");
                }
                else
                {
                    // User is not a librarian
                    return Forbid(); // Or return another status code for unauthorized access
                }
            }

            return Unauthorized(); // If the user is not authenticated
        }

        //[HttpPost]
        //[Route("Login")]
        //public ActionResult<List<LibrarianDto>> Login([FromBody] LibrarianDto librarian)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        // Authenticate librarian (sample logic)
        //        var authenticatedLibrarian = _librarianServices.Authenticate(librarian.Person.UserName, librarian.Person.Password);
        //        if (authenticatedLibrarian.Count ==0)
        //        {
        //            // If authentication fails
        //            return Unauthorized("Invalid username or password");
        //        }

        //        // If authenticated, you might generate a token or perform further actions as needed
        //        // For example, setting some authentication flag or creating a JWT token

        //        return Ok(authenticatedLibrarian); // Return the authenticated librarian DTO
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it as per your requirement
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}



    }
}
