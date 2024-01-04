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

        void Create(PersonDto dto)
        {
            Person person = _mapper.Map<Person>(dto);
            _repository.Person.Create(person);
            _repository.Save();

        }

        void Delete(PersonDto dto)
        {
            Person person = _mapper.Map<Person>(dto);
            _repository.Person.Delete(person);
            _repository.Save();
        }

        IList<PersonDto> FindAll()
        {

            List<Person> personsFromDB = _repository.Person.FindAll().ToList();
            List<PersonDto> personsDto = personsFromDB.Select(person => _mapper.Map<PersonDto>(person)).ToList();

            return personsDto;
        }

         PersonDto FindByCondition(int id)
        {
            Person personsFromDB = _repository.Person.FindByCondition(x=>x.Id == id).FirstOrDefault();
            PersonDto personsDto = _mapper.Map<PersonDto>(personsFromDB);
            return personsDto;
        }

        void Update(PersonDto dto)
        {
            Person person = _mapper.Map<Person>(dto);
            _repository.Person.Update(person);
            _repository.Save();
        }
    }
}
