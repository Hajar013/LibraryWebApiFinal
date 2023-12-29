using LibraryWebApiFinal.Models;

namespace DAL
{
    public class PersonDAL
    {
        private readonly AppDBContext _context;
        public PersonDAL(AppDBContext context)
        {
            _context= context;
        }
        public List<Person> GetAllPersons()
        {
            return _context.Persons.ToList();

        }

    }
}