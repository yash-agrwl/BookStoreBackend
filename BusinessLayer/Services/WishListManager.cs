using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.WishListModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Manager
{
    public class WishListManager : IWishListManager
    {
        private readonly IWishListRepository _repository;

        public WishListManager(IWishListRepository repository)
        {
            this._repository = repository;
        }

        public ResponseModel<WishListInfoModel> AddToWishList(int userId, int bookId)
        {
            try
            {
                return this._repository.AddToWishList(userId, bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<List<WishListInfoModel>> GetAllWishListItems(int userId)
        {
            try
            {
                return this._repository.GetAllWishListItems(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<string> RemoveFromWishList(int userId, int bookId)
        {
            try
            {
                return this._repository.RemoveFromWishList(userId, bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
