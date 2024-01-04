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
    //[Authorize(Policy = "AccounterPolicy")]
    public class AccounterController : ControllerBase
    {
        private readonly IAccounterService _accounterServices;
        private readonly IMapper _mapper;
        private readonly IAccounterService _accounterService;
        public AccounterController(IAccounterService accounterServices, IMapper mapper,
             IAccounterService accounterService)
        {
            _accounterServices = accounterServices;
            _mapper = mapper;
            _accounterService = accounterService;
        }
 
        [HttpGet("GetAccounters")]
        public IList<AccounterDto> Get()
        {
            var accounters = _accounterServices.FindAll();
            return accounters;
        }

        [HttpGet]
        [Route("GetAccounter/{id}")]
        public AccounterDto GetById(int id)
        {
            var accounter = _accounterServices.FindByCondition(id);
            return accounter;
        }

        [HttpPost("AllowBill")] 
        public IActionResult AllowBorrow(int billId)
        {
                var accounterId = GetUserIdFromClaim();

                // Validate librarian DTO or handle validation errors
                if (_accounterService.AllowBills(accounterId, billId))

                    return StatusCode(200);

                return StatusCode(400);

        } 

        private int GetUserIdFromClaim()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                return userId;
            }
            throw new InvalidOperationException();
        }

    }
}
