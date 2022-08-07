using CommonLayer;
using CommonLayer.WishListModel;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface IWishListRepository
    {
        ResponseModel<WishListInfoModel> AddToWishList(int userId, int bookId);
        ResponseModel<List<WishListInfoModel>> GetAllWishListItems(int userId);
        WishListInfoModel GetBookFromWishList(int userId, int bookId);
        ResponseModel<string> RemoveFromWishList(int userId, int bookId);
    }
}