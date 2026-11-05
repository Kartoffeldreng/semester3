using PersonApp.Models;

namespace PersonApp.Repository {
    public interface IPersonRepository {

        IEnumerable<Person> GetAll();

        Person? GetById(int Id);

        void Add(Person person);

        void Save();


    }
}
