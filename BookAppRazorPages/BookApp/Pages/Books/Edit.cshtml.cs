using BookApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookApp.Models.Books;

namespace BookApp.Pages.Books {

  public class EditModel : PageModel {
    private readonly IBookRepository _bookRepository;

    public EditModel(IBookRepository bookRepository) {
      _bookRepository = bookRepository;
    }

    [BindProperty]
    public BookFormViewModel Input { get; set; } = new();

    public IActionResult OnGet(int id) {
      Book? book = _bookRepository.GetById(id);
      if (book is null)
        return NotFound();

      Input.Copy(book);
      return Page();
    }

    public IActionResult OnPost(int id) {
      if (!ModelState.IsValid)
        return Page();

      Book? existing = _bookRepository.GetById(id);

      if (existing is null)
        return NotFound();

      existing.Copy(Input);
      _bookRepository.Update(existing);

      TempData["Success"] = $"Bogen \"{existing.Title}\" er opdateret.";
      return RedirectToPage("./Index");
    }


  }
}
