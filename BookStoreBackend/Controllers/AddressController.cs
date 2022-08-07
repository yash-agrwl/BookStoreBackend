using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.AddressModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreBackend.Controllers
{
    [ApiController]
    [Authorize(Roles = Role.User)]
    [Route("api/[Controller]")]
    public class AddressController : Controller
    {      
        private readonly IAddressManager _manager;

        public AddressController(IAddressManager manager)
        {
            this._manager = manager;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddAddress(AddAddressModel address)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this._manager.AddAddress(address, userId);

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
        public IActionResult UpdateAddress(AddressInfoModel address)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this._manager.UpdateAddress(address, userId);

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
        public IActionResult GetAllAddresses()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this._manager.GetAllAddresses(userId);

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
        public IActionResult DeleteAddressById(int addressId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this._manager.DeleteAddressById(addressId, userId);

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
