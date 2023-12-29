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

namespace BLL.Services.BillServices
{
    public class BillService : IBillService
    {
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;

        public BillService(IRepositoryFactory repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(BillDto dto)
        {
            Bill bill = _mapper.Map<Bill>(dto);
            _repository.Bill.Create(bill);

        }

        public void Delete(BillDto dto)
        {
            Bill bill = _mapper.Map<Bill>(dto);
            _repository.Bill.Delete(bill);

        }

        public IQueryable<BillDto> FindAll()
        {

            IQueryable<Bill> billsFromDB = _repository.Bill.FindAll();
            IQueryable<BillDto> billsDto = billsFromDB.Select(bill => _mapper.Map<BillDto>(bill));

            return billsDto;
        }

        public IQueryable<BillDto> FindByCondition(int id)
        {

            IQueryable<Bill> billsFromDB = _repository.Bill.FindByCondition(x => x.BillNo == id);
            IQueryable<BillDto> billsDto = billsFromDB.Select(bill => _mapper.Map<BillDto>(bill));

            return billsDto;
        }

        public void Update(BillDto dto)
        {
            Bill bill = _mapper.Map<Bill>(dto);
            _repository.Bill.Update(bill);

        }
    }
}
