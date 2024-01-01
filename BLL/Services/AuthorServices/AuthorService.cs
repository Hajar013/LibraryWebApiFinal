using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repositories.RepositoryFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.AuthorServices
{
    public class AuthorService: IAuthorService
    {
        private readonly IRepositoryFactory _repository;
        private readonly IMapper _mapper;

        public AuthorService(IRepositoryFactory repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(AuthorDto dto)
        {
            Author author = _mapper.Map<Author>(dto);
            _repository.Author.Create(author);

        }

        public void Delete(AuthorDto dto)
        {
            Author author = _mapper.Map<Author>(dto);
            _repository.Author.Delete(author);
        }

        public IQueryable<AuthorDto> FindAll()
        {

            IQueryable<Author> authorsFromDB = _repository.Author.FindAll();
            IQueryable<AuthorDto> authorsDto = authorsFromDB.Select(author => _mapper.Map<AuthorDto>(author));

            return authorsDto;
        }

        public IQueryable<AuthorDto> FindByCondition(int id)
        {

            IQueryable<Author> authorsFromDB = _repository.Author.FindByCondition(x => x.Id == id);
            IQueryable<AuthorDto> authorsDto = authorsFromDB.Select(author => _mapper.Map<AuthorDto>(author));

            return authorsDto;
        }

        public void Update(AuthorDto dto)
        {
            Author author = _mapper.Map<Author>(dto);
            _repository.Author.Update(author);
        }
    }
}
