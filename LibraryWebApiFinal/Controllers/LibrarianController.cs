using AutoMapper;
using BLL.DTOs;
using BLL.Services.LibrarianServices;
using BLL.Services.PersonServices;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryWebApiFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrarianController : ControllerBase
    {

        private readonly ILibrarianService _librarianServices;
        private readonly IMapper _mapper;
        public LibrarianController(ILibrarianService librarianServices, IMapper mapper)
        {
            _librarianServices = librarianServices;
            _mapper = mapper;
        }

        [HttpGet]
        public IQueryable<LibrarianDto> Get()
        {
            var librarians = _librarianServices.FindAll();
            return librarians;
        }

        [HttpGet]
        [Route("GetPerson/{id}")]
        public IQueryable<LibrarianDto> GetById(int id)
        {
            var librarian = _librarianServices.FindByCondition(id);
            return librarian;
        }


        [HttpPost]
        [Route("Register")]
        public ActionResult<LibrarianDto> PostPerson([FromBody] LibrarianDto librarian)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _librarianServices.Create(librarian);

                // Assuming your Create method sets the Id of the created person, you can retrieve it
                int createdlibrarianId = librarian.Id;

                // Use the correct action name ("GetById") and route values (id)
                return CreatedAtAction("GetById", new { id = createdlibrarianId }, librarian);
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
                existingLibrarian.LiberianLicenseNumber = updatedLibrarian.LiberianLicenseNumber;
              

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


    }
}
