using BookApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookApp.Models.Books;
using System.Net.Http.Headers;

namespace BookApp.Pages.Books {

  public class DetailsModel : PageModel {
    private readonly IBookRepository _bookRepository;

    public DetailsModel(IBookRepository bookRepository) {
      _bookRepository = bookRepository;
    }

    public Book? Book { get; private set; }

    public IActionResult OnGet(int id) {
      Book = _bookRepository.GetById(id);

      if (Book is null)
        return NotFound();

      return Page();
    }
  }
}
