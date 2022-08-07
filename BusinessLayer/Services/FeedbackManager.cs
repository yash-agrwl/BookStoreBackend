using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.FeedbackModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Manager
{
    public class FeedbackManager : IFeedbackManager
    {
        private readonly IFeedbackRepository _repository;

        public FeedbackManager(IFeedbackRepository repository)
        {
            this._repository = repository;
        }

        public ResponseModel<AddFeedbackModel> AddFeedback(AddFeedbackModel feedbackData, int userId)
        {
            try
            {
                return this._repository.AddFeedback(feedbackData, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<List<FeedbackInfoModel>> GetAllFeedbacks(int bookId)
        {
            try
            {
                return this._repository.GetAllFeedbacks(bookId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<EditFeedbackModel> EditFeedback(EditFeedbackModel feedbackData, int userId)
        {
            try
            {
                return this._repository.EditFeedback(feedbackData, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
