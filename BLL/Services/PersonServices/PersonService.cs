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
            //var _configPerson = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonDto>().ReverseMap());
            //_mapper = new Mapper(_configPerson);
            _mapper = mapper;
        }

        public void Create(PersonDto dto)
        {
            throw new NotImplementedException();
        }

        public void Delete(PersonDto dto)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PersonDto> FindAll()
        {
            //IQueryable<Person> personsFromDB = _repository.Person.FindAll();
            //IQueryable<PersonDto> personsDto = personsFromDB.Select(person => new PersonDto
            //{
            //    Id = person.Id,
            //    Role = person.Role,
            //    Name = person.Name,
            //    Address = person.Address,
            //    UserName = person.UserName,
            //    Password = person.Password,
            //});
            IQueryable<Person> personsFromDB = _repository.Person.FindAll();
            var personsDto = _mapper.ProjectTo<PersonDto>(personsFromDB);

            return personsDto;
        }

        //public IQueryable<PersonDto> FindByCondition(Expression<Func<PersonDto, bool>> expression)
        //{
        //    Expression<Func<Person, bool>> entityExpression = person =>
        //        expression.Compile().Invoke(new PersonDto
        //        {
        //            Id = person.Id,
        //            Role = person.Role,
        //            Name = person.Name,
        //            Address = person.Address,
        //            UserName = person.UserName,
        //            Password = person.Password,
        //        });

        //    IQueryable<Person> personsFromDB = _repository.Person.FindByCondition(entityExpression);
        //    IQueryable<PersonDto> personsDto = personsFromDB.Select(person => new PersonDto
        //    {
        //        Id = person.Id,
        //        Role = person.Role,
        //        Name = person.Name,
        //        Address = person.Address,
        //        UserName = person.UserName,
        //        Password = person.Password,
        //    });

        //    return personsDto;
        //}

        public IQueryable<PersonDto> FindByCondition(int id)
        {

            IQueryable<Person> personsFromDB = _repository.Person.FindByCondition(x=>x.Id == id);
            IQueryable<PersonDto> personsDto = personsFromDB.Select(person => new PersonDto
            {
                Id = person.Id,
                Role = person.Role,
                Name = person.Name,
                Address = person.Address,
                UserName = person.UserName,
                Password = person.Password,
            });

            return personsDto;
        }

        public void Update(PersonDto dto)
        {
            //var personsDto = new Person
            //{
            //    Id = dto.Id,
            //    Role = dto.Role,
            //    Name = dto.Name,
            //    Address = dto.Address,
            //    UserName = dto.UserName,
            //    Password = dto.Password,
            //};
            //var personEntity = _mapper.Map<Person>(dto);
            //var person = _repository.Person.Update(personEntity);
            //throw new NotImplementedException();
        }
    }
}
