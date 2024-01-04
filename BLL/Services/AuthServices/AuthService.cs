using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repositories.RepositoryFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;
        public AuthService (IConfiguration configuration, IRepositoryFactory repository, IMapper mapper)
        {
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
        }

        public string Authenticate(string username, string password)
        {
            var person = _repository.Person
                .FindByCondition(person =>
                    person.UserName == username && person.Password == password)
                .FirstOrDefault();
            if (person != null)
            {

                int id = GetPersonIdByRole(person);
                var token = GenerateJwtToken(person.Role, id);
                return token;
            }
            return null;
        }

        public string GenerateJwtToken(string Role, int Id)
        {
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                    new Claim(ClaimTypes.Role, Role),
                },
                expires: DateTime.Now.AddDays(1), 
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public string RegisterLibrarianDto(LibrarianDto librarianDto)
        //{
        //    librarianDto.Person.Role = "librarian";
        //    var librarian = _mapper.Map<Librarian>(librarianDto);
        //    _repository.Librarian.Create(librarian);
        //    _repository.Save();
        //    var token = GenerateJwtToken(librarianDto.Person.Role,librarian.Id);
        //    Console.WriteLine($"== inside register auth services: {librarian.Id} and personId {librarian.PersonId} and {librarian.Person.Id}===============");
        //    Console.WriteLine($"inside register {librarian.Id}");
        //    return token;
        //}



        public string Register(PersonDto user)
        {
            
            int id;
            var person = _mapper.Map<Person>(user);

            if (person.Librarian != null)
            {
                person.Role = "librarian";
                _repository.Librarian.Create(person.Librarian);
            }
            else if (person.Accounter != null)
            {
                 person.Role = "accounter";
                _repository.Accounter.Create(person.Accounter);
            }
            else if (person.Borrower != null)
            {
                person.Role = "borrower";
                _repository.Borrower.Create(person.Borrower);
            }


            _repository.Person.Create(person);
            _repository.Save();
            id = GetPersonIdByRole(person);
            var token = GenerateJwtToken(person.Role, person.Borrower.Id);
            return token;
        }

        private int GetPersonIdByRole(Person person)
        {
            int personId;

            switch (person.Role.ToLower())
            {
                case "librarian":
                    var librarian = _repository.Librarian.FindByCondition(l => l.PersonId == person.Id).FirstOrDefault();
                    personId = librarian.Id;
                    break;

                case "accounter":
                    var accounter = _repository.Accounter.FindByCondition(a => a.PersonId == person.Id).FirstOrDefault();
                    personId = accounter.Id;
                    break;

                case "borrower":
                    var borrower = _repository.Borrower.FindByCondition(b => b.PersonId == person.Id).FirstOrDefault();
                    personId = borrower.Id;
                    break;

                default:
                    return 0;
            }

            return personId;
        }


    }
}
