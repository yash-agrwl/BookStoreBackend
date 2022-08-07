using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.CartModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Manager
{
    public class CartManager : ICartManager
    {
        private readonly ICartRepository _repository;

        public CartManager(ICartRepository repository)
        {
            this._repository = repository;
        }

        public ResponseModel<CartInfoModel> AddToCart(int userId, int bookId)
        {
            try
            {
                return this._repository.AddToCart(userId, bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<List<CartInfoModel>> GetAllCartItems(int userId)
        {
            try
            {
                return this._repository.GetAllCartItems(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<string> RemoveFromCart(int userId, int bookId)
        {
            try
            {
                return this._repository.RemoveFromCart(userId, bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<string> UpdateCart(int userId, int bookId, int count)
        {
            try
            {
                return this._repository.UpdateCart(userId, bookId, count);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
