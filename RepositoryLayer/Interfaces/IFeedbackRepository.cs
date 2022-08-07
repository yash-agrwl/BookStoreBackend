using CommonLayer;
using CommonLayer.FeedbackModel;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface IFeedbackRepository
    {
        ResponseModel<AddFeedbackModel> AddFeedback(AddFeedbackModel feedbackData, int userId);
        ResponseModel<EditFeedbackModel> EditFeedback(EditFeedbackModel feedbackData, int userId);
        ResponseModel<List<FeedbackInfoModel>> GetAllFeedbacks(int userId);
    }
}