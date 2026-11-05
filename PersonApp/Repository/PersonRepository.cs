using Microsoft.EntityFrameworkCore;
using PersonApp.Data;
using PersonApp.Models;

namespace PersonApp.Repository {
    public class PersonRepository : IPersonRepository {

        private readonly AppDbContext _db;

        public PersonRepository(AppDbContext db) {
            _db = db;
        }

        public IEnumerable<Person> GetAll() =>
           _db.Persons.AsNoTracking().ToList();

        public Person? GetById(int id) =>
          _db.Persons.AsNoTracking().FirstOrDefault(p => p.Id == id);

        public void Add(Person person) =>
          _db.Persons.Add(person);

        public void Save() =>
          _db.SaveChanges();
    }
}
