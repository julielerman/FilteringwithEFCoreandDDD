using ContractBC.ContractAggregate;
using ContractBC.ValueObjects;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContractingUI
{
    public class DemoTasks
    {
        ContractContext _context;
        public DemoTasks(ContractContext context)
        {
            _context = context;
        }
        public void Seed()
        {
            _context.Database.EnsureCreated();
            if (_context.Contracts.Count()== 0) {
                var c1 = new Contract(
            DateTime.Today.AddDays(-10), new List<Author> { Author.UnsignedAuthor("Stonna", "Edelman", "s@stonna.com", "800-stonna") }, "A Book"
                );
                var c2 = new Contract(
     DateTime.Today.AddDays(-2), new List<Author> { Author.UnsignedAuthor("Julie", "Lerman", "j@julie.com", "800-julie") }, "Another Book");
 
                c2.AddAuthor(Author.UnsignedAuthor("Roland", "Guijt", "r@roland.com", "800-roland"));
                c2.CreateRevisionUsingSameSpecs
                    (ContractBC.Enums.ModReason.Other, "Changing title", "Learning Razor Tricks", c2.CurrentVersion().AuthorsCopy(),null);
                var c3 = new Contract(
     DateTime.Today, new List<Author> { Author.UnsignedAuthor("Liz", "Lemon", "l@liz.com", "800-liz") }, "Book II");

                //var optionsBuilder = new DbContextOptionsBuilder<ContractContext>();
                //optionsBuilder.UseSqlServer(
                //     "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=PubServiceTests");
                //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging();
                //var contractContext = new ContractContext(optionsBuilder.Options);
                _context.Database.EnsureDeleted();
                _context.Database.Migrate();
                _context.AddRange(c1, c2, c3);
                _context.SaveChanges();
            }

        }
    }
}
