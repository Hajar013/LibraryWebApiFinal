//using DAL.Entities;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using DAL.Repositories.RepositoryFactory;
//using BLL.Services.PersonServices;
//using BLL.DTOs;
//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace LibraryWebApiFinal.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    [Authorize]
//    public class PersonController : ControllerBase
//    {
    
//        private readonly IPersonService _personServices;
//        public PersonController(IPersonService personServices)
//        {
//            _personServices = personServices;
//        }

//        [HttpGet]
//        public List<PersonDto> Get()
//        {
//            var persons = _personServices.FindAll();
//            return persons;
//        }

//        [HttpGet]
//        [Route("GetPerson/{id}")]
//        public PersonDto GetById(int id)
//        {
//            var person = _personServices.FindByCondition(id);
//            return person;
//        }

//    }
//}
