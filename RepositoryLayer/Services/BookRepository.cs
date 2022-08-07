using CommonLayer;
using CommonLayer.BookModel;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace RepositoryLayer.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        private readonly MySqlConnection _con = new MySqlConnection();

        public BookRepository(IConfiguration configuration)
        {
            this._config = configuration;
            this._connectionString = _config.GetConnectionString("BookStoreDB");
            this._con.ConnectionString = this._connectionString;
        }

        public ResponseModel<AddBookModel> AddBook(AddBookModel bookData)
        {
            try
            {
                var result = new ResponseModel<AddBookModel>();

                var command = new MySqlCommand("sp_AddBook", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@name", bookData.BookName);
                command.Parameters.AddWithValue("@author", bookData.Author);
                command.Parameters.AddWithValue("@img", bookData.Image);
                command.Parameters.AddWithValue("@detail", bookData.BookDetail);
                command.Parameters.AddWithValue("@discount", bookData.DiscountPrice);
                command.Parameters.AddWithValue("@actual", bookData.ActualPrice);
                command.Parameters.AddWithValue("@qty", bookData.Quantity);

                _con.Open();
                var rowsAffected = command.ExecuteNonQuery();
                _con.Close();

                if (rowsAffected < 1)
                {
                    result.Message = "Unsuccessful to Add Book";
                    return result;
                }

                result.Status = true;
                result.Message = "Book Added Successfully";
                result.Data = bookData;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<BookInfoModel> UpdateBook(BookInfoModel bookData)
        {
            try
            {
                var result = new ResponseModel<BookInfoModel>();

                var command = new MySqlCommand("sp_UpdateBook", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@bid", bookData.BookID);
                command.Parameters.AddWithValue("@name", bookData.BookName);
                command.Parameters.AddWithValue("@author", bookData.Author);
                command.Parameters.AddWithValue("@img", bookData.Image);
                command.Parameters.AddWithValue("@detail", bookData.BookDetail);
                command.Parameters.AddWithValue("@discount", bookData.DiscountPrice);
                command.Parameters.AddWithValue("@actual", bookData.ActualPrice);
                command.Parameters.AddWithValue("@qty", bookData.Quantity);
                command.Parameters.AddWithValue("@rating", bookData.Rating);
                command.Parameters.AddWithValue("@count", bookData.RatingCount);

                _con.Open();
                var rowsAffected = command.ExecuteNonQuery();
                _con.Close();

                if (rowsAffected < 1)
                {
                    result.Message = "Unsuccessful to Update Book";
                    return result;
                }

                result.Status = true;
                result.Message = "Book Updated Successfully";
                result.Data = bookData;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<BookInfoModel> DeleteBook(int bookId)
        {
            try
            {
                var result = new ResponseModel<BookInfoModel>();

                var command = new MySqlCommand("sp_DeleteBook", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@b_id", bookId);

                _con.Open();
                var rowsAffected = command.ExecuteNonQuery();
                _con.Close();

                if (rowsAffected < 1)
                {
                    result.Message = "Unsuccessful to Delete Book";
                    return result;
                }

                result.Status = true;
                result.Message = "Book Deleted Successfully";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<BookInfoModel> GetBookById(int bookId)
        {
            try
            {
                var result = new ResponseModel<BookInfoModel>();

                var command = new MySqlCommand("sp_GetBookById", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@b_id", bookId);

                _con.Open();
                var dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        result.Data = GetBookDetails(dataReader);
                    }

                    _con.Close();
                    result.Status = true;
                    result.Message = "Book Retrived Successfully";
                    return result;
                }
                else
                {
                    _con.Close();
                    result.Message = "Book not Available";
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<List<BookInfoModel>> GetAllBooks()
        {
            try
            {
                var result = new ResponseModel<List<BookInfoModel>>();

                var command = new MySqlCommand("sp_GetAllBooks", _con);
                command.CommandType = CommandType.StoredProcedure;

                _con.Open();
                var dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    result.Data= new List<BookInfoModel>();

                    while (dataReader.Read())
                    {
                        result.Data.Add( GetBookDetails(dataReader) );
                    }

                    _con.Close();
                    result.Status = true;
                    result.Message = $"{result.Data.Count} Books Retrived Successfully";
                    return result;
                }
                else
                {
                    _con.Close();
                    result.Message = "Books not Available";
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static BookInfoModel GetBookDetails(MySqlDataReader dataReader)
        {
            var book = new BookInfoModel();

            book.BookID = Convert.ToInt32(dataReader["BookID"]);
            book.BookName = Convert.ToString(dataReader["BookName"]);
            book.Author = Convert.ToString(dataReader["Author"]);
            book.Image = Convert.ToString(dataReader["BookImage"]);
            book.BookDetail = Convert.ToString(dataReader["BookDetail"]);
            book.DiscountPrice = (float) dataReader["DiscountPrice"];
            book.ActualPrice = (float) dataReader["ActualPrice"];
            book.Quantity = Convert.ToInt32(dataReader["Quantity"]);
            book.Rating = (float) dataReader["Rating"];
            book.RatingCount = Convert.ToInt32(dataReader["RatingCount"]);

            return book;
        }
    }
}
