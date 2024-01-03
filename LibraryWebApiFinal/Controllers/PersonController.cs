using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using DAL.Repositories.RepositoryFactory;
using BLL.Services.PersonServices;
using BLL.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryWebApiFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PersonController : ControllerBase
    {
    
        private readonly IPersonService _personServices;
        public PersonController(IPersonService personServices)
        {
            _personServices = personServices;
        }

        [HttpGet]
        public List<PersonDto> Get()
        {
            var persons = _personServices.FindAll();
            return persons;
        }

        [HttpGet]
        [Route("GetPerson/{id}")]
        public List<PersonDto> GetById(int id)
        {
            var person = _personServices.FindByCondition(id);
            return person;
        }


        //[HttpPut]
        //[Route("Edit/{id}")]
        //public ActionResult<PersonDto> EditPerson(int id, [FromBody] PersonDto updatedPerson)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var existingPerson = _personServices.FindByCondition(id).FirstOrDefault();

        //        if (existingPerson == null)
        //        {
        //            return NotFound($"Person with ID {id} not found");
        //        }

        //        // Update properties of existingPerson with values from updatedPerson
        //        existingPerson.Name = updatedPerson.Name;
        //        existingPerson.Address = updatedPerson.Address;
        //        existingPerson.UserName = updatedPerson.UserName;
        //        existingPerson.Password = updatedPerson.Password;

        //        // Use AutoMapper to map the updated entity back to DTO if needed
        //        var updatedPersonDto = _mapper.Map<PersonDto>(existingPerson);

        //        // Save changes to the repository
        //        _personServices.Update(existingPerson);

        //        return Ok(updatedPersonDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it as per your requirement
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        //[HttpDelete]
        //[Route("Delete/{id}")]
        //public IActionResult DeletePerson(int id)
        //{
        //    try
        //    {
        //        var existingPerson = _personServices.FindByCondition(id).FirstOrDefault();

        //        if (existingPerson == null)
        //        {
        //            return NotFound($"Person with ID {id} not found");
        //        }

        //        // Use your service to delete the person
        //        _personServices.Delete(existingPerson);

        //        return NoContent(); // 204 No Content
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it as per your requirement
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}



    }
}
