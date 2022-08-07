using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.AdminModel;
using CommonLayer.UserModel;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreBackend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminManager _manager;

        public AdminController(IAdminManager manager)
        {
            this._manager = manager;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel adminData)
        {
            try
            {
                var result = this._manager.Login(adminData);

                if (result.Status == true)
                {
                    string token = this._manager.GenerateToken(result.Data.AdminID, result.Data.EmailID);
                    return this.Ok(new
                    {
                        result.Status,
                        result.Message,
                        token,
                        result.Data
                    });
                }

                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetByEmail")]
        public IActionResult GetAdminByEmail(string email)
        {
            try
            {
                var result = new ResponseModel<AdminInfoModel>();
                result.Data = this._manager.GetAdminByEmail(email);

                if (result.Data != null)
                {
                    result.Status = true;
                    result.Message = "Admin Data Retrived Successfully";
                    return this.Ok(result);
                }

                result.Message = "Unsuccessful to retrieve Admin Data";
                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }
    }
}
