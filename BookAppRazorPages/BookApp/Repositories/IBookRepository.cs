using BookApp.Models.Books;

namespace BookApp.Repositories;

public interface IBookRepository {
  IEnumerable<Book> GetAll();
  Book? GetById(int id);
  Book Add(Book book);
  bool Update(Book book);
  bool Delete(int id);
  bool Exists(int id);
}