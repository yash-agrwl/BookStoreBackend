using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.BookModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreBackend.Controllers
{
    [ApiController]
    [Authorize(Roles = Role.Admin)]
    [Route("api/[Controller]")]
    public class BookController : Controller
    {
        private readonly IBookManager _manager;

        public BookController(IBookManager manager)
        {
            this._manager = manager;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddBook(AddBookModel bookData)
        {
            try
            {
                var result = this._manager.AddBook(bookData);

                if (result.Status == true)
                {
                    return this.Ok(result);
                }

                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        [HttpPatch]
        [Route("Update")]
        public IActionResult UpdateBook(BookInfoModel bookData)
        {
            try
            {
                var result = this._manager.UpdateBook(bookData);

                if (result.Status == true)
                {
                    return this.Ok(result);
                }

                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                var result = this._manager.DeleteBook(bookId);

                if (result.Status == true)
                {
                    return this.Ok(result);
                }

                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetBookById(int bookId)
        {
            try
            {
                var result = this._manager.GetBookById(bookId);

                if (result.Status == true)
                {
                    return this.Ok(result);
                }

                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = this._manager.GetAllBooks();

                if (result.Status == true)
                {
                    return this.Ok(result);
                }

                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }
    }
}
