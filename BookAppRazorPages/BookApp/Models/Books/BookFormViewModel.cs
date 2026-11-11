using System.ComponentModel.DataAnnotations;

namespace BookApp.Models.Books;

public class BookFormViewModel : BookBase {
  [Required(ErrorMessage = "Titel er påkrævet.")]
  [StringLength(200, MinimumLength = 2, ErrorMessage = "Titel skal være mellem 2 og 200 tegn.")]
  public override string Title { get; set; } = string.Empty;

  [Required(ErrorMessage = "Forfatter er påkrævet.")]
  [StringLength(100, ErrorMessage = "Forfatter skal være højst 100 tegn.")]
  public override string Author { get; set; } = string.Empty;

  [Required(ErrorMessage = "Sidetal er påkrævet.")]
  [Range(1, 10000, ErrorMessage = "Sidetal skal være mellem 1 og 10000.")]
  public override int Pages { get; set; }

  [Required(ErrorMessage = "Pris er påkrævet.")]
  [Range(0.0, 9999.99, ErrorMessage = "Pris skal være mellem 0 og 9999,99.")]
  public override decimal Price { get; set; }

  [Required(ErrorMessage = "Udgivelsesår er påkrævet.")]
  [Range(1000, 2099, ErrorMessage = "Udgivelsesår skal være mellem 1000 og 2099.")]
  public override int Published { get; set; } = DateTime.Today.Year;
}