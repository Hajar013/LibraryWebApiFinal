using AutoMapper;
using BLL.DTOs;
using BLL.Services.AccounterServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BLL.Services.BillServices;
using BLL.Services.AuthServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryWebApiFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AccounterPolicy")]
    public class AccounterController : ControllerBase
    {
        private readonly IAccounterService _accounterServices;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IAccounterService _accounterService;
        public AccounterController(IAccounterService accounterServices, IMapper mapper, IAuthService authService,
             IAccounterService accounterService)
        {
            _accounterServices = accounterServices;
            _mapper = mapper;
            _authService = authService;
            _accounterService = accounterService;
        }
 
        [HttpGet("GetAccounters")]
        public IQueryable<AccounterDto> Get()
        {
            var accounters = _accounterServices.FindAll();
            return accounters;
        }

        [HttpGet]
        [Route("GetAccounter/{id}")]
        public IQueryable<AccounterDto> GetById(int id)
        {
            var accounter = _accounterServices.FindByCondition(id);
            return accounter;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] AccounterDto accounterDto)
        {
            var accounter = _authService.Authenticate(accounterDto.Person.UserName, accounterDto.Person.Password);

            if (accounter == null)
                return Unauthorized("Invalid username or password");

            var token = _authService.GenerateJwtToken(accounter);
            return Ok(new { Token = token });
        }


        //[HttpPost("Register")]
        //[AllowAnonymous]
        //public IActionResult Register([FromBody] AccounterDto accounter)
        //{
        //    // Validate accounter DTO or handle validation errors

        //    accounter.Person.Role = "accounter";

        //    try
        //    {
        //        _authService.Register(accounter);
        //        //_accounterServices.Create(accounter);

        //        // Assuming your Create method sets the Id of the created accounter, you can retrieve it
        //        int createdAccounterId = accounter.Id;

        //        var token = _authService.GenerateJwtToken(accounter);

        //        return CreatedAtAction("GetById", new { id = createdAccounterId }, new { Token = token });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it as per your requirement
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}



        [HttpPut]
        [Route("Edit/{id}")]
        public ActionResult<AccounterDto> EditPerson(int id, [FromBody] AccounterDto updatedAccounter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingAccounter = _accounterServices.FindByCondition(id).FirstOrDefault();

                if (existingAccounter == null)
                {
                    return NotFound($"Accounter with ID {id} not found");
                }

                // Update properties of existingAccounter with values from updatedAccounter
                existingAccounter.Certification = updatedAccounter.Certification;


                // Use AutoMapper to map the updated entity back to DTO if needed
                var updatedAccounterDto = _mapper.Map<AccounterDto>(existingAccounter);

                // Save changes to the repository
                _accounterServices.Update(existingAccounter);

                return Ok(updatedAccounterDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your requirement
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult DeleteAccounter(int id)
        {
            try
            {
                var existingAccounter = _accounterServices.FindByCondition(id).FirstOrDefault();

                if (existingAccounter == null)
                {
                    return NotFound($"Accounter with ID {id} not found");
                }

                // Use your service to delete the Accounter
                _accounterServices.Delete(existingAccounter);

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your requirement
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AllowBill")] 
        public IActionResult AllowBorrow(int billId)
        {
            try
            {
                var accounterId = GetUserIdFromClaim();

                // Validate librarian DTO or handle validation errors
                if (_accounterService.AllowBills(accounterId, billId))

                    return StatusCode(200, "The borrower's request to bill has been approved.");

                return StatusCode(400, "The borrower's request to bill has been failed");
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

    }
}
