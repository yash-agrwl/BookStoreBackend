using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.OrderModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreBackend.Controllers
{
    [ApiController]
    [Authorize(Roles = Role.User)]
    [Route("api/[Controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderManager _manager;

        public OrderController(IOrderManager manager)
        {
            this._manager = manager;
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public IActionResult PlaceOrder(PlaceOrderModel orderData)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this._manager.PlaceOrder(orderData, userId);

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
        [Route("GetAll")]
        public IActionResult GetAllOrders()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this._manager.GetAllOrders(userId);

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
