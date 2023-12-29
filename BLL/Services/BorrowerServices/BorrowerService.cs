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

namespace BLL.Services.BorrowerServices
{
    public  class BorrowerService : IBorrowerService
    {
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;

        public BorrowerService(IRepositoryFactory repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(BorrowerDto dto)
        {
            Borrower borrower = _mapper.Map<Borrower>(dto);
            _repository.Borrower.Create(borrower);
           
        }

        public void Delete(BorrowerDto dto)
        {
            Borrower borrower = _mapper.Map<Borrower>(dto);
            _repository.Borrower.Delete(borrower);
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
        }
    }
}
