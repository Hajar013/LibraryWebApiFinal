using DAL;
using LibraryWebApiFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL
{
    public class PersonBLL
    {
        private readonly PersonDAL _personDAL;
        public PersonBLL(PersonDAL personDAL)
        {
            _personDAL= personDAL;

        }
        public List<Person> GetAllPersons()
        {
            return _personDAL.GetAllPersons();

        }
    }
}