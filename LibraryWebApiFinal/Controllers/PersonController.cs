using LibraryWebApiFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryWebApiFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly AppDBContext _context;
        public PersonController(AppDBContext context)
        {
            _context = context;
        }


        // GET: api/<PersonController>
        [HttpGet]
        [Route("get all persons")]
        public ActionResult<IEnumerable<Person>> Get()
        {
            return _context.Persons.ToList();

        }

        // GET api/<PersonController>/5
        [HttpGet]
        [Route("get person by id ")]
        public Person GetPerson(int id)
        {
            Person p = new Person();
            p = _context.Persons.FirstOrDefault(x => x.Id == id);
            if (p == null)
                throw new Exception("not found ");
            return p;
        }

        [HttpPost]
        [Route("add new person ")]
        public ActionResult<Person> PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Persons.Add(person);
                _context.SaveChanges(); // Saving changes synchronously

                return CreatedAtAction("GetPerson", new { id = person.Id }, person);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your requirement
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        //// PUT api/<PersonController>/5
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
