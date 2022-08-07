using CommonLayer;
using CommonLayer.BookModel;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface IBookRepository
    {
        ResponseModel<AddBookModel> AddBook(AddBookModel bookData);
        ResponseModel<BookInfoModel> UpdateBook(BookInfoModel bookData);
        ResponseModel<BookInfoModel> DeleteBook(int bookId);
        ResponseModel<BookInfoModel> GetBookById(int bookId);
        ResponseModel<List<BookInfoModel>> GetAllBooks();
    }
}