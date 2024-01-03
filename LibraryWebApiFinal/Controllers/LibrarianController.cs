using AutoMapper;
using BLL.DTOs;
using BLL.Services.AuthorServices;
using BLL.Services.AuthServices;
using BLL.Services.BookServices;
using BLL.Services.LibrarianServices;
using BLL.Services.PersonServices;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;


namespace LibraryWebApiFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "LibrarianPolicy")]
    public class LibrarianController : ControllerBase
    {

        private readonly ILibrarianService _librarianServices;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public LibrarianController(ILibrarianService librarianServices, IMapper mapper, IAuthService authService, IBookService bookService)
        {
            _librarianServices = librarianServices;
            _mapper = mapper;
            _authService = authService;
            _bookService = bookService;
        }

        [HttpPost("AddBook")]
        public void AddBook([FromBody] BookDto book)
        {
            _bookService.Create(book);
        }

        [HttpGet]
        [Route("GetBookById/{id}")]
        public BookDto GetBookById(int id)
        {
            // Retrieve the book by id and return it
            var book = _bookService.FindByCondition(id);
            return book;
        }
         
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

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LibrarianDto librarianDto)
        {
            var librarian = _authService.Authenticate(librarianDto.Person.UserName, librarianDto.Person.Password);

            if (librarian == null)
                return Unauthorized("Invalid username or password");

            var token = _authService.GenerateJwtToken(librarian);
            return Ok(new { Token = token });
        }

        //[AllowAnonymous]
        //[HttpPost("Register")]
        //public IActionResult Register([FromBody] LibrarianDto librarian)
        //{
        //    // Validate librarian DTO or handle validation errors

        //    librarian.Person.Role = "librarian";

        //    try
        //    {
        //        _authService.Register(librarian);
        //        //_librarianServices.Create(librarian);

        //        // Assuming your Create method sets the Id of the created librarian, you can retrieve it
        //        int createdLibrarianId = librarian.Id;

        //        var token = _authService.GenerateJwtToken(librarian);

        //        return CreatedAtAction("GetById", new { id = createdLibrarianId }, new { Token = token });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it as per your requirement
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}



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
   
        [HttpPost("AllowBorrow")]
        public IActionResult AllowBorrow(int tranisactionId)
        {
            try
            {
                var librarianId = GetUserIdFromClaim();

                // Validate librarian DTO or handle validation errors
                if (_librarianServices.AllowBorrow(librarianId, tranisactionId))

                    return StatusCode(200, "The borrower's request has been approved.");

                return StatusCode(400, "The borrower's request has been failed");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An internal server error occurred.: {ex.Message}");
            }
        }

        [HttpPost("AllowReturn")]
        public IActionResult AllowReturn(int tranisactionId)
        {
            try
            {
                var librarianId = GetUserIdFromClaim();

                // Validate librarian DTO or handle validation errors
                if (_librarianServices.AllowReturn(librarianId, tranisactionId))

                    return StatusCode(200, "The borrower's request has been approved.");

                return StatusCode(400, "The borrower's request has been failed");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An internal server error occurred.: {ex.Message}");
            }
        }
        private int GetUserIdFromClaim()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                return userId;
            }
            // Handle the case where the claim doesn't contain a valid user ID
            throw new InvalidOperationException("User ID not found in claims.");
        }

        [HttpPost("CreatBookWithAuther")]
        public IActionResult CreatBookWithAuther([FromBody]BookAuthorDto bookAuthorDto)
        {
            try
            {
                
                _librarianServices.AddBookAndAuther(bookAuthorDto);

                    return StatusCode(200, "AddBookAndAuther.");

            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An internal server error occurred.: {ex.Message}");
            }
        }


    }
}
