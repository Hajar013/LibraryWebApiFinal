﻿using DAL.Entities;
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
        private readonly IMapper _mapper;
        public PersonController(IPersonService personServices, IMapper mapper)
        {
            _personServices = personServices;
            _mapper = mapper;
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

    }
}
