using ContractBC.ContractAggregate;
using ContractBC.ValueObjects;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
            if (_context.Contracts.Count() == 0)
            {
                var c1 = new Contract(DateTime.Today.AddDays(-10),
                  new List<Author> { Author.UnsignedAuthor("Stonna", "Edelman", "s@stonna.com", "800-stonna") },
                  "title tbd"
                  );

                var c2 = new Contract(DateTime.Today.AddDays(-2),
                    new List<Author> { Author.UnsignedAuthor("Julie", "Lerman", "j@julie.com", "800-julie") },
                    "Another Book without a title"){
                    };
                c2.AddAuthor(Author.UnsignedAuthor("Roland", "Guijt", "r@roland.com", "800-roland"));
                c2.CreateRevisionUsingSameSpecs
                    (ContractBC.Enums.ModReason.Other, "Changing title", "Learning Razor Tricks", c2.CurrentVersion().AuthorsCopy(), null);
                
                var c3 = new Contract(DateTime.Today,
                  new List<Author> { Author.UnsignedAuthor("Liz", "Lemon", "l@liz.com", "800-liz") }, 
                  "I'm funny! Live with it!");
                var c4 = new Contract(DateTime.Today.AddDays(10),
                  new List<Author> { Author.UnsignedAuthor("Rhoda", "Lerman", "r@lerman.com", "800-rhoda") },
                  "Solimeos"
                  );
                var c5 = new Contract(DateTime.Today.AddDays(10),
                  new List<Author> { Author.UnsignedAuthor("Beth", "Cato", "b@cato.com", "800-bethc") },
                  "A Thousand Recipes for Revenge"
                  );
                var c6 = new Contract(DateTime.Today.AddDays(-15),
                  new List<Author> { Author.UnsignedAuthor("Laline", "Paul", "l@buzzbuzz.com", "800-buzzzz") },
                  "The Bees"
                  );
                var c7 = new Contract(DateTime.Today.AddDays(20),
                  new List<Author> { Author.UnsignedAuthor("Alix E.", "Harrow", "ae@harrow.com", "800-alexe") },
                  "The Ten Thousand Doors of January"
                  );
                _context.Database.EnsureDeleted();
                _context.Database.Migrate();
                _context.AddRange(c1, c2, c3,c4,c5,c6,c7);
                _context.SaveChanges();
            }

        }
    }
}
