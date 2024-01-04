using AutoMapper;
using BLL.DTOs;
using BLL.Services.AuthServices;
using BLL.Services.BillServices;
using BLL.Services.BookServices;
using DAL.Entities;
using DAL.Repositories.RepositoryFactory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.AccounterServices
{
    public class AccounterService : IAccounterService
    {
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;
        private readonly IBillService _billService;
        private readonly IBookService _bookService;

        public AccounterService(IRepositoryFactory repository, IMapper mapper, 
            IBillService billService, IBookService bookService)
        {
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

        public List<AccounterDto> FindAll()
        {

            List<Accounter> accountersFromDB = _repository.Accounter.FindAll().ToList();
            List<AccounterDto> accountersDto = accountersFromDB.Select(accounter => _mapper.Map<AccounterDto>(accounter)).ToList();

            return accountersDto;
        }

        public AccounterDto FindByCondition(int id)
        {

            Accounter accountersFromDB = _repository.Accounter.FindByCondition(x => x.Id == id).FirstOrDefault();
            AccounterDto accountersDto =  _mapper.Map<AccounterDto>(accountersFromDB);

            return accountersDto;
        }

        public void Update(AccounterDto dto)
        {
            Accounter accounter = _mapper.Map<Accounter>(dto);
            _repository.Accounter.Update(accounter);
            _repository.Save();
        }


        public bool AllowBills(int accounterId, int billId)
        {
            var bill = _billService.FindByCondition(billId);
            //BookDto book = bill.Book;
            if (bill == null && bill.Book == null)
            {
                return false;
            }

            if (!bill.Book.Availability || bill.Book.Copies == 0)
            {
                Console.WriteLine(" B NAV COPIES 0");

                _billService.Delete(bill);

                return false;
            }

            if (bill != null && bill.Status == "Pending")
            {

                bill.Book.Copies--;
                if (bill.Book.Copies == 0)
                {
                    bill.Book.Availability = false;
                }
                _bookService.Update(bill.Book);

                bill.AccounterId = accounterId;
                bill.Status = "Success";
                _billService.Update(bill);

                return true;
            }

            return false;

        }
    }
}
