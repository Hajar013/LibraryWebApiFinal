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

namespace BLL.Services.TransactionServices
{
    public  class TransactionService : ITransactionService
    {
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;

        public TransactionService(IRepositoryFactory repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(TransactionDto dto)
        {
            Transaction transaction = _mapper.Map<Transaction>(dto);
            _repository.Transaction.Create(transaction);
            _repository.Save();
           
        }

        public void Delete(TransactionDto dto)
        {
            Transaction transaction = _mapper.Map<Transaction>(dto);
            _repository.Transaction.Delete(transaction);
            _repository.Save();
        }

        IList<TransactionDto> FindAll()
        {

            List<Transaction> transactionsFromDB = _repository.Transaction.FindAll().ToList();
            List<TransactionDto> transactionsDto = transactionsFromDB.Select(transaction => _mapper.Map<TransactionDto>(transaction)).ToList();

            return transactionsDto;
        }

        public TransactionDto FindByCondition(int id)
        {

            Transaction transactionsFromDB = _repository.Transaction.FindByCondition(x=>x.Id == id).FirstOrDefault();
            TransactionDto transactionsDto =  _mapper.Map<TransactionDto>(transactionsFromDB);

            return transactionsDto;
        }

        public void Update(TransactionDto dto)
        {
            Transaction transaction = _mapper.Map<Transaction>(dto);
            _repository.Transaction.Update(transaction);
            _repository.Save();
        }
    }
}
