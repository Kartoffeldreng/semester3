using BookApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookApp.Models.Books;

namespace BookApp.Pages.Books {

  public class CreateModel : PageModel {
    private readonly IBookRepository _bookRepository;

    public CreateModel(IBookRepository bookRepository) {
      _bookRepository = bookRepository;
    }

    [BindProperty]
    public BookFormViewModel Input { get; set; } = new();

    public void OnGet() {
    }

    public IActionResult OnPost() {
      if (!ModelState.IsValid)
        return Page();

      Book book = new();
      book.Copy(Input);
      _bookRepository.Add(book);


      TempData["Success"] = $"Bogen \"{book.Title}\" er oprettet.";
      return RedirectToPage(nameof(Index));
    }

    
  }
}
