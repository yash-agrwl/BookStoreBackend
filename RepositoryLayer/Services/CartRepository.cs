using CommonLayer;
using CommonLayer.CartModel;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace RepositoryLayer.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        private readonly MySqlConnection _con = new MySqlConnection();
        private readonly IBookRepository _bookRepo;

        public CartRepository(IConfiguration configuration, IBookRepository bookRepo)
        {
            this._config = configuration;
            this._connectionString = _config.GetConnectionString("BookStoreDB");
            this._con.ConnectionString = this._connectionString;
            this._bookRepo = bookRepo;
        }

        public ResponseModel<CartInfoModel> AddToCart(int userId, int bookId)
        {
            try
            {
                var result = new ResponseModel<CartInfoModel>();
                var existBook = GetBookFromCart(userId, bookId);

                if (existBook == null)
                {
                    var command = new MySqlCommand("sp_AddToCart", _con);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@u_id", userId);
                    command.Parameters.AddWithValue("@b_id", bookId);

                    _con.Open();
                    var rowsAffected = command.ExecuteNonQuery();
                    _con.Close();

                    if (rowsAffected < 1)
                    {
                        result.Message = "Unsuccessful To Add Book To Cart";
                        return result;
                    }

                    var addedData = GetBookFromCart(userId, bookId);

                    result.Status = true;
                    result.Message = "Book Successfully Added To Cart";
                    result.Data = addedData;
                    return result;
                }

                result.Message = "Book Already Exist In Cart";
                result.Data = existBook;
                return result;            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CartInfoModel GetBookFromCart(int userId, int bookId)
        {
            var command = new MySqlCommand("sp_GetBookFromCart", _con);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@u_id", userId);
            command.Parameters.AddWithValue("@b_id", bookId);

            _con.Open();
            var dataReader = command.ExecuteReader();

            if (dataReader.HasRows)
            {
                var cartData = new CartInfoModel();

                while (dataReader.Read())
                {
                    cartData.CartID = Convert.ToInt32(dataReader["CartID"]);
                    cartData.UserID = Convert.ToInt32(dataReader["UserID"]);
                    cartData.BookID = Convert.ToInt32(dataReader["BookID"]);
                    cartData.BookCount = Convert.ToInt32(dataReader["BookCount"]);
                }

                _con.Close();
                var book = _bookRepo.GetBookById(bookId);
                cartData.BookData = book.Data;
                return cartData;
            }
            else
            {
                _con.Close();
                return null;
            }
        }

        public ResponseModel<List<CartInfoModel>> GetAllCartItems(int userId)
        {
            try
            {
                var result = new ResponseModel<List<CartInfoModel>>();

                var command = new MySqlCommand("sp_GetAllCartItems", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@u_id", userId);

                _con.Open();
                var dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    result.Data = new List<CartInfoModel>();

                    while (dataReader.Read())
                    {
                        var cartData = new CartInfoModel();
                        cartData.CartID = Convert.ToInt32(dataReader["CartID"]);
                        cartData.UserID = Convert.ToInt32(dataReader["UserID"]);
                        cartData.BookID = Convert.ToInt32(dataReader["BookID"]);
                        cartData.BookCount = Convert.ToInt32(dataReader["BookCount"]);
                        var book = _bookRepo.GetBookById(cartData.BookID);
                        cartData.BookData = book.Data;
                        result.Data.Add(cartData);
                    }

                    _con.Close();
                    result.Status = true;
                    result.Message = $"{result.Data.Count} Books Retrived Successfully";
                    return result;
                }
                else
                {
                    _con.Close();
                    result.Message = "Cart is Empty";
                    return result;
                }
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
                var result = new ResponseModel<string>();

                var command = new MySqlCommand("sp_RemoveFromCart", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@u_id", userId);
                command.Parameters.AddWithValue("@b_id", bookId);

                _con.Open();
                var rowsAffected = command.ExecuteNonQuery();
                _con.Close();

                if (rowsAffected < 1)
                {
                    result.Message = "Unsuccessful To Remove Book From Cart";
                    return result;
                }

                result.Status = true;
                result.Message = "Book Successfully Removed From Cart";
                return result;
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
                if (count == 0)
                {
                    return RemoveFromCart(userId, bookId);
                }

                var result = new ResponseModel<string>();

                var command = new MySqlCommand("sp_UpdateCart", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@u_id", userId);
                command.Parameters.AddWithValue("@b_id", bookId);
                command.Parameters.AddWithValue("@count", count);

                _con.Open();
                var rowsAffected = command.ExecuteNonQuery();
                _con.Close();

                if (rowsAffected < 1)
                {
                    result.Message = "Unsuccessful To Update Book Quantity";
                    return result;
                }

                result.Status = true;
                result.Message = "Book Quantity Successfully Updated";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
