using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repositories.RepositoryFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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

        public PersonDto Authenticate(string username, string password)
        {
            var authenticatedPerson = _repository.Person
                .FindByCondition(person =>
                    person.UserName == username && person.Password == password)
                .FirstOrDefault();
            var librarian = _mapper.Map<PersonDto>(authenticatedPerson);

            return librarian;

        }

        public string GenerateJwtToken(PersonDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                },
                expires: DateTime.Now.AddDays(1), // Token expiry time (adjust as needed)
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public void Register(PersonDto user)
        {
            if (user.Librarian != null)
            {
                user.Role = "librarian";
            }
            else if (user.Accounter != null)
            {
                user.Role = "accounter";
            }
            else
            {
                user.Role = "borrower";
            }
            var person = _mapper.Map<Person>(user);
            _repository.Person.Create(person);
            _repository.Save();

        }
    }
}
