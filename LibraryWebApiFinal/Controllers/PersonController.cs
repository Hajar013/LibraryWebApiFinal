using LibraryWebApiFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using DAL.Repositories.RepositoryFactory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryWebApiFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
       private readonly IRepositoryFactory _repository;

        //   public PersonController(AppDBContext context)
        public PersonController(IRepositoryFactory repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
           // var domesticAccounts = _repository.Person.FindByCondition(x => x.Id.Equals("Domestic"));
            var persons = _repository.Person.FindAll();
            return persons;
        }
        // GET: api/<PersonController>
        //[HttpGet]
        //[Route("get all persons")]
        //public ActionResult<IEnumerable<Person>> Get()
        //{
        //    return _BLL.GetAllPersons();

        //}

        //// GET api/<PersonController>/5
        //[HttpGet]
        //[Route("get person by id ")]
        //public Person GetPerson(int id)
        //{
        //    Person p = new Person();
        //    p = _context.Persons.FirstOrDefault(x => x.Id == id);
        //    if (p == null)
        //        throw new Exception("not found ");
        //    return p;
        //}

        //[HttpPost]
        //[Route("add new person ")]
        //public ActionResult<Person> PostPerson(Person person)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        _context.Persons.Add(person);
        //        _context.SaveChanges(); // Saving changes synchronously

        //        return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it as per your requirement
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
        ////// PUT api/<PersonController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<PersonController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
