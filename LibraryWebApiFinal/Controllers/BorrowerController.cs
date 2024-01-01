using AutoMapper;
using BLL.DTOs;
using BLL.Services.BorrowerServices;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryWebApiFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        private readonly IBorrowerService _borrowerServices;
        private readonly IMapper _mapper;
        private readonly IAuthService<BorrowerDto> _authService;
        public BorrowerController(IBorrowerService borrowerServices, IMapper mapper, IAuthService<BorrowerDto> authService)
        {
            _borrowerServices = borrowerServices;
            _mapper = mapper;
            _authService = authService;
        }
        //[Authorize]
        [Authorize(Policy = "BorrowerPolicy")]
        [HttpGet("GetBorrowers")]
        public IQueryable<BorrowerDto> Get()
        {
            var borrowers = _borrowerServices.FindAll();
            return borrowers;
        }

        [HttpGet]
        [Route("GetBorrower/{id}")]
        public IQueryable<BorrowerDto> GetById(int id)
        {
            var borrower = _borrowerServices.FindByCondition(id);
            return borrower;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] BorrowerDto borrowerDto)
        {
            var borrower = _authService.Authenticate(borrowerDto.Person.UserName, borrowerDto.Person.Password);

            if (borrower == null)
                return Unauthorized("Invalid username or password");

            var token = _authService.GenerateJwtToken(borrower);
            return Ok(new { Token = token });
        }


        [HttpPost("Register")]
        public IActionResult Register([FromBody] BorrowerDto borrower)
        {
            // Validate borrower DTO or handle validation errors

            borrower.Person.Role = "borrower";

            try
            {
                _authService.Register(borrower);
                //_borrowerServices.Create(borrower);

                // Assuming your Create method sets the Id of the created borrower, you can retrieve it
                int createdBorrowerId = borrower.Id;

                var token = _authService.GenerateJwtToken(borrower);

                return CreatedAtAction("GetById", new { id = createdBorrowerId }, new { Token = token });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your requirement
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPut]
        [Route("Edit/{id}")]
        public ActionResult<BorrowerDto> EditPerson(int id, [FromBody] BorrowerDto updatedBorrower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingBorrower = _borrowerServices.FindByCondition(id).FirstOrDefault();

                if (existingBorrower == null)
                {
                    return NotFound($"Borrower with ID {id} not found");
                }

                // Update properties of existingBorrower with values from updatedBorrower
                existingBorrower.DateOfMembership = updatedBorrower.DateOfMembership;


                // Use AutoMapper to map the updated entity back to DTO if needed
                var updatedBorrowerDto = _mapper.Map<BorrowerDto>(existingBorrower);

                // Save changes to the repository
                _borrowerServices.Update(existingBorrower);

                return Ok(updatedBorrowerDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your requirement
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult DeleteBorrower(int id)
        {
            try
            {
                var existingBorrower = _borrowerServices.FindByCondition(id).FirstOrDefault();

                if (existingBorrower == null)
                {
                    return NotFound($"Borrower with ID {id} not found");
                }

                // Use your service to delete the Borrower
                _borrowerServices.Delete(existingBorrower);

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your requirement
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("CheckBorrowerRole")]
        public ActionResult CheckBorrowerRole()
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Check if the user has the "borrower" role claim
                var isBorrower = User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "borrower");

                if (isBorrower)
                {
                    // User is a borrower
                    return Ok("User is a borrower.");
                }
                else
                {
                    // User is not a borrower
                    return Forbid(); // Or return another status code for unauthorized access
                }
            }

            return Unauthorized(); // If the user is not authenticated
        }

    }
}
