using CommonLayer;
using CommonLayer.WishListModel;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface IWishListManager
    {
        ResponseModel<WishListInfoModel> AddToWishList(int userId, int bookId);
        ResponseModel<List<WishListInfoModel>> GetAllWishListItems(int userId);
        ResponseModel<string> RemoveFromWishList(int userId, int bookId);
    }
}