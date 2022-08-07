using CommonLayer;
using CommonLayer.WishListModel;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace RepositoryLayer.Repository
{
    public class WishListRepository : IWishListRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        private readonly MySqlConnection _con = new MySqlConnection();
        private readonly IBookRepository _bookRepo;

        public WishListRepository(IConfiguration configuration, IBookRepository bookRepo)
        {
            this._config = configuration;
            this._connectionString = _config.GetConnectionString("BookStoreDB");
            this._con.ConnectionString = this._connectionString;
            this._bookRepo = bookRepo;
        }

        public ResponseModel<WishListInfoModel> AddToWishList(int userId, int bookId)
        {
            try
            {
                var result = new ResponseModel<WishListInfoModel>();
                var existBook = GetBookFromWishList(userId, bookId);

                if (existBook == null)
                {
                    var command = new MySqlCommand("sp_AddToWishList", _con);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@u_id", userId);
                    command.Parameters.AddWithValue("@b_id", bookId);

                    _con.Open();
                    var rowsAffected = command.ExecuteNonQuery();
                    _con.Close();

                    if (rowsAffected < 1)
                    {
                        result.Message = "Unsuccessful To Add Book To WishList";
                        return result;
                    }

                    var addedData = GetBookFromWishList(userId, bookId);

                    result.Status = true;
                    result.Message = "Book Successfully Added To WishList";
                    result.Data = addedData;
                    return result;
                }

                result.Message = "Book Already Exist In WishList";
                result.Data = existBook;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public WishListInfoModel GetBookFromWishList(int userId, int bookId)
        {
            var command = new MySqlCommand("sp_GetBookFromWishList", _con);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@u_id", userId);
            command.Parameters.AddWithValue("@b_id", bookId);

            _con.Open();
            var dataReader = command.ExecuteReader();

            if (dataReader.HasRows)
            {
                var wishListData = new WishListInfoModel();

                while (dataReader.Read())
                {
                    wishListData.WishListID = Convert.ToInt32(dataReader["WishListID"]);
                    wishListData.UserID = Convert.ToInt32(dataReader["UserID"]);
                    wishListData.BookID = Convert.ToInt32(dataReader["BookID"]);
                }

                _con.Close();
                var book = _bookRepo.GetBookById(bookId);
                wishListData.BookData = book.Data;
                return wishListData;
            }
            else
            {
                _con.Close();
                return null;
            }
        }

        public ResponseModel<List<WishListInfoModel>> GetAllWishListItems(int userId)
        {
            try
            {
                var result = new ResponseModel<List<WishListInfoModel>>();

                var command = new MySqlCommand("sp_GetAllWishListItems", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@u_id", userId);

                _con.Open();
                var dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    result.Data = new List<WishListInfoModel>();

                    while (dataReader.Read())
                    {
                        var wishListData = new WishListInfoModel();
                        wishListData.WishListID = Convert.ToInt32(dataReader["WishListID"]);
                        wishListData.UserID = Convert.ToInt32(dataReader["UserID"]);
                        wishListData.BookID = Convert.ToInt32(dataReader["BookID"]);
                        var book = _bookRepo.GetBookById(wishListData.BookID);
                        wishListData.BookData = book.Data;
                        result.Data.Add(wishListData);
                    }

                    _con.Close();
                    result.Status = true;
                    result.Message = $"{result.Data.Count} Books Retrived Successfully";
                    return result;
                }
                else
                {
                    _con.Close();
                    result.Message = "WishList is Empty";
                    return result;
                }
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
                var result = new ResponseModel<string>();

                var command = new MySqlCommand("sp_RemoveFromWishList", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@u_id", userId);
                command.Parameters.AddWithValue("@b_id", bookId);

                _con.Open();
                var rowsAffected = command.ExecuteNonQuery();
                _con.Close();

                if (rowsAffected < 1)
                {
                    result.Message = "Unsuccessful To Remove Book From WishList";
                    return result;
                }

                result.Status = true;
                result.Message = "Book Successfully Removed From WishList";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
