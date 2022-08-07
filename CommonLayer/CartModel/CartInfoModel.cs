using CommonLayer.BookModel;

namespace CommonLayer.CartModel
{
    public class CartInfoModel
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public BookInfoModel BookData { get; set; }
        public int BookCount { get; set; }
    }
}
