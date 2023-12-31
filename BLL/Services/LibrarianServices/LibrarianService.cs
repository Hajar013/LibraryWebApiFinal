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

namespace BLL.Services.LibrarianServices
{
    public  class LibrarianService : ILibrarianService
    {
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;

        public LibrarianService(IRepositoryFactory repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(LibrarianDto dto)
        {
            Librarian librarian = _mapper.Map<Librarian>(dto);
            _repository.Librarian.Create(librarian);
            _repository.Save();

        }

        public void Delete(LibrarianDto dto)
        {
            Librarian librarian = _mapper.Map<Librarian>(dto);
            _repository.Librarian.Delete(librarian);
            _repository.Save();
        }

        public IQueryable<LibrarianDto> FindAll()
        {

            IQueryable<Librarian> librariansFromDB = _repository.Librarian.FindAll();
            IQueryable<LibrarianDto> librariansDto = librariansFromDB.Select(librarian => _mapper.Map<LibrarianDto>(librarian));

            return librariansDto;
        }

        public IQueryable<LibrarianDto> FindByCondition(int id)
        {

            IQueryable<Librarian> librariansFromDB = _repository.Librarian.FindByCondition(x=>x.Id == id);
            IQueryable<LibrarianDto> librariansDto = librariansFromDB.Select(librarian => _mapper.Map<LibrarianDto>(librarian));

            return librariansDto;
        }

        public void Update(LibrarianDto dto)
        {
            Librarian librarian = _mapper.Map<Librarian>(dto);
            _repository.Librarian.Update(librarian);
            _repository.Save();
        }
        public List<LibrarianDto> Authenticate(string username, string password)
        {
            // Replace this logic with your actual authentication process (e.g., database check, token validation, etc.)
            var authenticatedLibrarian = _repository.Librarian.FindByCondition(librarian =>
                librarian.Person.UserName == username && librarian.Person.Password == password);
             var authenticatedLibrarianDto = _mapper.Map<List<LibrarianDto>>(authenticatedLibrarian);
            return authenticatedLibrarianDto;
        }

    }
}
