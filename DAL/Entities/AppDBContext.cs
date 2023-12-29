using System.Collections.Generic;
using System.Transactions;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryWebApiFinal.Models
{
    public class AppDBContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDBContext(IConfiguration configuration, DbContextOptions<AppDBContext> options) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DbCon"));
        }

        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Accounter> Accounters { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<BookAuthor> AuthorBooks { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
