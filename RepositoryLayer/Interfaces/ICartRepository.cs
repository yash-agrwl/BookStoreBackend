using CommonLayer;
using CommonLayer.CartModel;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface ICartRepository
    {
        ResponseModel<CartInfoModel> AddToCart(int userId, int bookId);
        ResponseModel<List<CartInfoModel>> GetAllCartItems(int userId);
        ResponseModel<string> UpdateCart(int userId, int bookId, int count);
        ResponseModel<string> RemoveFromCart(int bookId, int userId);
    }
}