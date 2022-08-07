using CommonLayer.BookModel;

namespace CommonLayer.WishListModel
{
    public class WishListInfoModel
    {
        public int WishListID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public BookInfoModel BookData { get; set; }
    }
}
