using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repositories.RepositoryFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.AccounterServices
{
    public class AccounterService
    {
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;

        public AccounterService(IRepositoryFactory repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(AccounterDto dto)
        {
            Accounter accounter = _mapper.Map<Accounter>(dto);
            _repository.Accounter.Create(accounter);

        }

        public void Delete(AccounterDto dto)
        {
            Accounter accounter = _mapper.Map<Accounter>(dto);
            _repository.Accounter.Delete(accounter);
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
        }
    }
}
