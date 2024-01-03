using DAL.Repositories.PersonRepos;
using DAL.Repositories;
using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.RepositoryFactory;
using System.Linq.Expressions;
using AutoMapper;
using DAL.Entities;

namespace BLL.Services.PersonServices
{
    public  class PersonService : IPersonService
    {
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;

        public PersonService(IRepositoryFactory repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(PersonDto dto)
        {
            Person person = _mapper.Map<Person>(dto);
            _repository.Person.Create(person);
            _repository.Save();

        }

        public void Delete(PersonDto dto)
        {
            Person person = _mapper.Map<Person>(dto);
            _repository.Person.Delete(person);
            _repository.Save();
        }

        public IQueryable<PersonDto> FindAll()
        {
        
            IQueryable<Person> personsFromDB = _repository.Person.FindAll();
            IQueryable<PersonDto> personsDto = personsFromDB.Select(person => _mapper.Map<PersonDto>(person));

            return personsDto;
        }

        public IQueryable<PersonDto> FindByCondition(int id)
        {
            IQueryable<Person> personsFromDB = _repository.Person.FindByCondition(x=>x.Id == id);
            IQueryable<PersonDto> personsDto = personsFromDB.Select(person => _mapper.Map<PersonDto>(person));
            return personsDto;
        }

        public void Update(PersonDto dto)
        {
            Person person = _mapper.Map<Person>(dto);
            _repository.Person.Update(person);
            _repository.Save();
        }
    }
}
