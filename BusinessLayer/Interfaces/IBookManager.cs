using CommonLayer;
using CommonLayer.BookModel;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface IBookManager
    {
        ResponseModel<AddBookModel> AddBook(AddBookModel bookData);
        ResponseModel<BookInfoModel> UpdateBook(BookInfoModel bookData);
        ResponseModel<BookInfoModel> DeleteBook(int bookId);
        ResponseModel<BookInfoModel> GetBookById(int bookId);
        ResponseModel<List<BookInfoModel>> GetAllBooks();
    }
}