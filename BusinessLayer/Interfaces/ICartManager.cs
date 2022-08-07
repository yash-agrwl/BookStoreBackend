using CommonLayer;
using CommonLayer.CartModel;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface ICartManager
    {
        ResponseModel<CartInfoModel> AddToCart(int userId, int bookId);
        ResponseModel<List<CartInfoModel>> GetAllCartItems(int userId);
        ResponseModel<string> UpdateCart(int userId, int bookId, int count);
        ResponseModel<string> RemoveFromCart(int userId, int bookId);
    }
}