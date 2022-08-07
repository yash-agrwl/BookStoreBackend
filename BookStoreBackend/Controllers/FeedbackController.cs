using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.FeedbackModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreBackend.Controllers
{
    [ApiController]
    [Authorize(Roles = Role.User)]
    [Route("api/[Controller]")]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackManager _manager;

        public FeedbackController(IFeedbackManager manager)
        {
            this._manager = manager;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddFeedback(AddFeedbackModel feedbackData)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this._manager.AddFeedback(feedbackData, userId);

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
        [AllowAnonymous]
        [Route("GetAll")]
        public IActionResult GetAllFeedbacks(int bookId)
        {
            try
            {
                var result = this._manager.GetAllFeedbacks(bookId);

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
        [Route("Edit")]
        public IActionResult EditFeedback(EditFeedbackModel feedbackData)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this._manager.EditFeedback(feedbackData, userId);

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
