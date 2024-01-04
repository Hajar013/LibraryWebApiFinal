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

         void Create(AuthorDto dto)
        {
            Author author = _mapper.Map<Author>(dto);
            _repository.Author.Create(author);

        }

         void Delete(AuthorDto dto)
        {
            Author author = _mapper.Map<Author>(dto);
            _repository.Author.Delete(author);
        }

         List<AuthorDto> FindAll()
        {

            IQueryable<Author> authorsFromDB = _repository.Author.FindAll();
            List<AuthorDto> authorsDto = authorsFromDB.Select(author => _mapper.Map<AuthorDto>(author)).ToList();

            return authorsDto;
        }

         AuthorDto FindByCondition(int id)
        {

            Author authorsFromDB = _repository.Author.FindByCondition(x => x.Id == id).FirstOrDefault();
            AuthorDto authorsDto =  _mapper.Map<AuthorDto>(authorsFromDB);

            return authorsDto;
        }

         void Update(AuthorDto dto)
        {
            Author author = _mapper.Map<Author>(dto);
            _repository.Author.Update(author);
        }
    }
}
