using BookApp.Models.Books;

namespace BookApp.Repositories;

public class BookRepository : IBookRepository {
  private readonly List<Book> _books;
  private int _nextId;

  public BookRepository() {
    _books = [];
    _nextId = 0;

    SeedData();
  }

  public IEnumerable<Book> GetAll() {
    return _books.ToList();
  }

  public Book? GetById(int id) {
    return _books.FirstOrDefault(b => b.Id == id);
  }

  public Book Add(Book book) {
    book.Id = _nextId++;
    _books.Add(book);
    return book;
  }

  public bool Update(Book book) {
    Book? existing = GetById(book.Id);

    if (existing is null)
      return false;

    existing.Title = book.Title;
    existing.Author = book.Author;
    existing.Pages = book.Pages;
    existing.Price = book.Price;
    existing.Published = book.Published;

    return true;
  }

  public bool Delete(int id) {
    Book? book = GetById(id);
    return book is not null && _books.Remove(book);
  }

  public bool Exists(int id) {
    return GetById(id) is not null;
  }

  private void SeedData() {
    Add(new Book {
      Title = "Dune",
      Author = "Frank Herbert",
      Pages = 412,
      Price = 149.95m,
      Published = 1965
    });
    Add(new Book {
      Title = "The Hobbit",
      Author = "J.R.R. Tolkien",
      Pages = 310,
      Price = 129.95m,
      Published = 1937
    });
    Add(new Book {
      Title = "Foundation",
      Author = "Isaac Asimov",
      Pages = 256,
      Price = 99.95m,
      Published = 1951
    });
  }
}