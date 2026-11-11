namespace BookApp.Models.Books;

public abstract class BookBase {
  public virtual string Title { get; set; } = string.Empty;
  public virtual string Author { get; set; } = string.Empty;
  public virtual int Pages { get; set; }
  public virtual decimal Price { get; set; }
  public virtual int Published { get; set; }

  public void Copy(BookBase source) {
    Title = source.Title;
    Author = source.Author;
    Pages = source.Pages;
    Price = source.Price;
    Published = source.Published;
  }
}