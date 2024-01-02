using AutoMapper;
using BLL.DTOs;
using BLL.Services.BillServices;
using BLL.Services.BookServices;
using DAL.Entities;
using DAL.Repositories.RepositoryFactory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.AccounterServices
{
    public class AccounterService : IAccounterService, IAuthService<AccounterDto>
    {
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IBillService _billService;
        private readonly IBookService _bookService;

        public AccounterService(IRepositoryFactory repository, IMapper mapper, IConfiguration configuration,
            IBillService billService, IBookService bookService)
        {
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
            _billService = billService;
            _bookService = bookService;
        }

        public void Create(AccounterDto dto)
        {
            Accounter accounter = _mapper.Map<Accounter>(dto);
            _repository.Accounter.Create(accounter);
            _repository.Save();
        }

        public void Delete(AccounterDto dto)
        {
            Accounter accounter = _mapper.Map<Accounter>(dto);
            _repository.Accounter.Delete(accounter);
            _repository.Save();
        }

        public IQueryable<AccounterDto> FindAll()
        {

            IQueryable<Accounter> accountersFromDB = _repository.Accounter.FindAll();
            IQueryable<AccounterDto> accountersDto = accountersFromDB.Select(accounter => _mapper.Map<AccounterDto>(accounter));

            return accountersDto;
        }

        public IQueryable<AccounterDto> FindByCondition(int id)
        {

            IQueryable<Accounter> accountersFromDB = _repository.Accounter.FindByCondition(x => x.Id == id);
            IQueryable<AccounterDto> accountersDto = accountersFromDB.Select(accounter => _mapper.Map<AccounterDto>(accounter));

            return accountersDto;
        }

        public void Update(AccounterDto dto)
        {
            Accounter accounter = _mapper.Map<Accounter>(dto);
            _repository.Accounter.Update(accounter);
            _repository.Save();
        }

        public string GenerateJwtToken(AccounterDto user)
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


        public AccounterDto Authenticate(string username, string password)
        {
            var authenticatedAccounter = _repository.Accounter
                .FindByCondition(accounter =>
                    accounter.Person.UserName == username && accounter.Person.Password == password)
                .Include(accounter => accounter.Person)
                .FirstOrDefault();
            var accounter = _mapper.Map<AccounterDto>(authenticatedAccounter);

            return accounter;

        }


        public void Register(AccounterDto user)
        {
            var accounter = _mapper.Map<Accounter>(user);
            _repository.Person.Create(accounter.Person);
            _repository.Accounter.Create(accounter);
            _repository.Save();

        }

        public bool AllowBills(int accounterId, int billId)
        {
            var bill = _billService.FindByCondition(billId).FirstOrDefault();
            BookDto book = _bookService.FindByCondition(bill.BookId);
            if (bill == null)
            {
                Console.WriteLine("bill NULL");
                return false;
            }

            if (book == null)
            {
                Console.WriteLine(" Book NULL");
                return false;

            }
            if (!book.Availability || book.Copies == 0)
            {
                Console.WriteLine(" B NAV COPIES 0");

                _billService.Delete(bill);

                return false;
            }

            if (bill != null && bill.Status == "Pending")
            {
                // Logic to approve the borrow request

                book.Copies--;
                if (book.Copies == 0)
                {
                    book.Availability = false;
                }
                _bookService.Update(book);

                bill.AccounterId = accounterId;
                bill.Status = "Success";
                _billService.Update(bill);

                return true;
            }

            return false;

        }
    }
}
