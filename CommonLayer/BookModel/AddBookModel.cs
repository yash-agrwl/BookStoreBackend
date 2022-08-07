using System.ComponentModel.DataAnnotations;

namespace CommonLayer.BookModel
{
    public class AddBookModel
    {
        [Required]
        public string BookName { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public float ActualPrice { get; set; }
        [Required]
        public float DiscountPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string BookDetail { get; set; }
    }
}
