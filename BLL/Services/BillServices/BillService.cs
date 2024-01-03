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
            _repository.Save();

        }

        public void Delete(BillDto dto)
        {
            Bill bill = _mapper.Map<Bill>(dto);
            _repository.Bill.Delete(bill);
            _repository.Save();
        }

        public List<BillDto> FindAll()
        {

            IQueryable<Bill> billsFromDB = _repository.Bill.FindAll();
            List<BillDto> billsDto = billsFromDB.Select(bill => _mapper.Map<BillDto>(bill)).ToList();

            return billsDto;
        }

        public BillDto FindByCondition(int id)
        {

            Bill billsFromDB = _repository.Bill.FindByCondition(x => x.BillNo == id).FirstOrDefault();
            BillDto billsDto = _mapper.Map<BillDto>(billsFromDB);

            return billsDto;
        }

        public void Update(BillDto dto)
        {
            Bill bill = _mapper.Map<Bill>(dto);
            _repository.Bill.Update(bill);
            _repository.Save();
        }
    }
}
