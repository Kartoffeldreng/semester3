using System.ComponentModel.DataAnnotations;

namespace PersonApp.Models {
    public class Person {

        [Required(ErrorMessage = "Navn er påkrævet")]
        [StringLength(100, ErrorMessage = "Navn må højst være 100 tegn")]
        public string Name { get; set; }
        
        public int Id { get; set; }

        public string ImageFileName { get; set; } = string.Empty;
    }
}
