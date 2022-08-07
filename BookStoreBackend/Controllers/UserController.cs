using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.UserModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreBackend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly IUserManager _manager;

        public UserController(IUserManager manager)
        {
            this._manager = manager;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Signup(RegisterModel userData)
        {
            try
            {
                var result = this._manager.Signup(userData);

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

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel userData)
        {
            try
            {
                var result = this._manager.Login(userData);

                if (result.Status == true)
                {
                    string token = this._manager.GenerateToken(result.Data.UserID, result.Data.EmailID);
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
        public IActionResult GetUserByEmail()
        {
            try
            {
                string email = User.Claims.FirstOrDefault(x => x.Type == "EmailId").Value;
                var result = new ResponseModel<UserInfoModel>();
                result.Data = this._manager.GetUserByEmail(email);

                if (result.Data != null)
                {
                    result.Status = true;
                    result.Message = "User Data Retrived Successfully";
                    return this.Ok(result);
                }

                result.Message = "Unsuccessful to retrieve User Data";
                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetUserById()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = new ResponseModel<UserInfoModel>();
                result.Data = this._manager.GetUserById(userId);

                if (result.Data != null)
                {
                    result.Status = true;
                    result.Message = "User Data Retrived Successfully";
                    return this.Ok(result);
                }

                result.Message = "Unsuccessful to retrieve User Data";
                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel userData)
        {
            try
            {
                var result = this._manager.ResetPassword(userData);

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

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                string result = this._manager.ForgotPassword(email);

                if (result.Equals("Email Sent Successfully"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result, Data = email });
                }

                return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }
    }
}
