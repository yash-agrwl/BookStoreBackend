namespace CommonLayer.BookModel
{
    public class BookInfoModel
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public float ActualPrice { get; set; }
        public float DiscountPrice { get; set; }
        public int Quantity { get; set; }
        public float Rating { get; set; }
        public int RatingCount { get; set; }
        public string Image { get; set; }
        public string BookDetail { get; set; }
    }
}
