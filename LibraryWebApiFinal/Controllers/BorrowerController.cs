using AutoMapper;
using BLL.DTOs;
using BLL.Services.BorrowerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BLL.Services.BookServices;
using BLL.Services.BillServices;
using BLL.Services.AuthServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryWebApiFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "BorrowerPolicy")]
    public class BorrowerController : ControllerBase
    {
        private readonly IBorrowerService _borrowerServices;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        public BorrowerController(IBorrowerService borrowerServices, IMapper mapper, IBookService bookService)
        {
            _borrowerServices = borrowerServices;
            _mapper = mapper;
            _bookService = bookService;
        }
 
        [HttpGet("GetBorrowers")]
        public IList<BorrowerDto> Get()
        {
            var borrowers = _borrowerServices.FindAll();
            return borrowers;
        }


        [HttpGet]
        [Route("GetBorrower/{id}")]
        public BorrowerDto GetById(int id)
        {
            var borrower = _borrowerServices.FindById(id);
            return borrower;
        }


        //[HttpPut]
        //[Route("Edit/{id}")]
        //public ActionResult<BorrowerDto> EditPerson(int id, [FromBody] BorrowerDto updatedBorrower)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var existingBorrower = _borrowerServices.FindById(id);

        //        if (existingBorrower == null)
        //        {
        //            return NotFound();
        //        }

        //        // Update properties of existingBorrower with values from updatedBorrower
        //        existingBorrower.DateOfMembership = updatedBorrower.DateOfMembership;


        //        // Use AutoMapper to map the updated entity back to DTO if needed
        //        var updatedBorrowerDto = _mapper.Map<BorrowerDto>(existingBorrower);

        //        // Save changes to the repository
        //        _borrowerServices.Update(existingBorrower);

        //        return Ok(updatedBorrowerDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500);
        //    }
        //}

        //[HttpDelete]
        //[Route("Delete/{id}")]
        //public IActionResult DeleteBorrower(int id)
        //{
        //    try
        //    {
        //        var existingBorrower = _borrowerServices.FindById(id);

        //        if (existingBorrower == null)
        //        {
        //            return NotFound();
        //        }

        //        // Use your service to delete the Borrower
        //        _borrowerServices.Delete(existingBorrower);

        //        return NoContent(); // 204 No Content
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it as per your requirement
        //        return StatusCode(500);
        //    }
        //}

        //[Authorize(Policy = "BorrowerPolicy")]
        //[HttpPost("RequestToBorrow")]
        //public IActionResult RequestToBorrow(int bookId)
        //{
        //    try
        //    {
        //        int borrowerId = GetUserIdFromClaim();
        //        Console.WriteLine(bookId + " " + borrowerId+"=========================================================");
        //        // Validate borrower DTO or handle validation errors
        //        if (_borrowerServices.RequestToBorrowBook(bookId, borrowerId))
        //            return StatusCode(200);

        //        return StatusCode(400,"RequestToBorrowBook return false");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500);
        //    }
        //} 

        [HttpPost("RequestToBorrow")]
        public IActionResult RequestToBorrow(int bookId)
        {
            try
            {
                int borrowerId = GetUserIdFromClaim();
                // Validate borrower DTO or handle validation errors
                if (_borrowerServices.RequestToBorrowBook(bookId, borrowerId))
                    return StatusCode(200);

                return StatusCode(400);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("RequestToReturn")]
        public IActionResult RequestToReturn(int transactionId)
        {
            try
            {
                int borrowerId = GetUserIdFromClaim();
                Console.WriteLine(transactionId + "  " + borrowerId + "======================================");
                // Validate borrower DTO or handle validation errors
                if (_borrowerServices.RequestToReturnBook(transactionId, borrowerId))
                    return StatusCode(200);

                return StatusCode(400);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("GetBook/{id}")]
        public BookDto GetBookById(int id)
        {
            var book = _bookService.FindByCondition(id);
            return book;
        }

        //[HttpGet]
        //[Route("GetBook/{title}")]
        //public BookDto GetBookBytitle(string title)
        //{
        //    var book = _bookService.FindByTitle(title).First(b=>b.Title == title);
        //    return book;
        //}

        // Helper method to get the user ID from the claim
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

        [Authorize(Policy = "BorrowerPolicy")]
        [HttpPost("RequestToBill")]
        public IActionResult RequestToBill(int bookId)
        {
            try
            {
                int borrowerId = GetUserIdFromClaim();
                // Validate borrower DTO or handle validation errors
                if (_borrowerServices.RequestToBill(bookId, borrowerId))
                    return StatusCode(200);

                return StatusCode(400);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

    }
}
