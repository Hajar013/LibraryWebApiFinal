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
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BLL.Services.BorrowerServices
{
    public  class BorrowerService : IBorrowerService, IAuthService<BorrowerDto>
    {
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public BorrowerService(IRepositoryFactory repository, IMapper mapper, IConfiguration configuration) 
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }


        public void Create(BorrowerDto dto)
        {
            Borrower borrower = _mapper.Map<Borrower>(dto);
            _repository.Borrower.Create(borrower);
            _repository.Save();

        }

        public void Delete(BorrowerDto dto)
        {
            Borrower borrower = _mapper.Map<Borrower>(dto);
            _repository.Borrower.Delete(borrower);
            _repository.Save();
        }

        public IQueryable<BorrowerDto> FindAll()
        {

            IQueryable<Borrower> borrowersFromDB = _repository.Borrower.FindAll();
            IQueryable<BorrowerDto> borrowersDto = borrowersFromDB.Select(borrower => _mapper.Map<BorrowerDto>(borrower));

            return borrowersDto;
        }

        public IQueryable<BorrowerDto> FindByCondition(int id)
        {

            IQueryable<Borrower> borrowersFromDB = _repository.Borrower.FindByCondition(x=>x.Id == id);
            IQueryable<BorrowerDto> borrowersDto = borrowersFromDB.Select(borrower => _mapper.Map<BorrowerDto>(borrower));

            return borrowersDto;
        }


        public void Update(BorrowerDto dto)
        {
            Borrower borrower = _mapper.Map<Borrower>(dto);
            _repository.Borrower.Update(borrower);
            _repository.Save();
        }

        public string GenerateJwtToken(BorrowerDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Person.Role),
                },
                expires: DateTime.Now.AddMinutes(30), // Token expiry time (adjust as needed)
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public BorrowerDto Authenticate(string username, string password)
        {
            var authenticatedBorrower = _repository.Borrower
                .FindByCondition(borrower =>
                    borrower.Person.UserName == username && borrower.Person.Password == password)
                .Include(borrower => borrower.Person)
                .FirstOrDefault();
            var borrower = _mapper.Map<BorrowerDto>(authenticatedBorrower);

            return borrower;

        }


        public void Register(BorrowerDto user)
        {
            var borrower = _mapper.Map<Borrower>(user);
            _repository.Person.Create(borrower.Person);
            _repository.Borrower.Create(borrower);
            _repository.Save();

        }

    }
}
