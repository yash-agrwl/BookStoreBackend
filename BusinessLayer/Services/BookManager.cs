using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.BookModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Manager
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _repository;

        public BookManager(IBookRepository repository)
        {
            this._repository = repository;
        }

        public ResponseModel<AddBookModel> AddBook(AddBookModel bookData)
        {
            try
            {
                return this._repository.AddBook(bookData);
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
                return this._repository.UpdateBook(bookData);
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
                return this._repository.DeleteBook(bookId);
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
                return this._repository.GetBookById(bookId);
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
                return this._repository.GetAllBooks();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
