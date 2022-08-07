using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreBackend.Controllers
{
    [ApiController]
    [Authorize(Roles = Role.User)]
    [Route("api/[Controller]")]
    public class WishListController : Controller
    {
        private readonly IWishListManager _manager;

        public WishListController(IWishListManager manager)
        {
            this._manager = manager;
        }

        [HttpPost]
        [Route("AddToWishList")]
        public IActionResult AddToWishList(int bookId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this._manager.AddToWishList(userId, bookId);

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

        [HttpGet]
        [Route("GetAllWishListItems")]
        public IActionResult GetAllWishListItems()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this._manager.GetAllWishListItems(userId);

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
        [Route("RemoveFromWishList")]
        public IActionResult RemoveFromWishList(int bookId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this._manager.RemoveFromWishList(userId, bookId);

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
