using BookApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookApp.Models.Books;

namespace BookApp.Pages.Books {

  public class DeleteModel : PageModel {
    private readonly IBookRepository _bookRepository;

    public DeleteModel(IBookRepository bookRepository) {
      _bookRepository = bookRepository;
    }

    public Book? Book { get; private set; }

    public IActionResult OnGet(int id) {
      Book = _bookRepository.GetById(id);

      if (Book is null)
        return NotFound();

      return Page();
    }

    public IActionResult OnPost(int id) {
      Book? book = _bookRepository.GetById(id);

      if (book is null)
        return NotFound();

      _bookRepository.Delete(id);

      TempData["Success"] = "Bogen er slettet.";

      return RedirectToPage("./Index");
    }


      
    }


  }
