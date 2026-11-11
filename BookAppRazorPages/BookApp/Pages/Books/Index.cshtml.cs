using BookApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookApp.Models.Books;

namespace BookApp.Pages.Books {

  public class IndexModel : PageModel {
    private readonly IBookRepository _bookRepository;

    public IndexModel(IBookRepository bookRepository) {
      _bookRepository = bookRepository;
    }

    public List<Book> Books { get; private set; } = [];

    public void OnGet() { 
      Books = _bookRepository.GetAll().ToList();
    }
  }
}
