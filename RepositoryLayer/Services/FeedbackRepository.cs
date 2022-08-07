using CommonLayer;
using CommonLayer.FeedbackModel;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace RepositoryLayer.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        private readonly MySqlConnection _con = new MySqlConnection();

        public FeedbackRepository(IConfiguration configuration)
        {
            this._config = configuration;
            this._connectionString = _config.GetConnectionString("BookStoreDB");
            this._con.ConnectionString = this._connectionString;
        }

        public ResponseModel<AddFeedbackModel> AddFeedback(AddFeedbackModel feedbackData, int userId)
        {
            try
            {
                var result = new ResponseModel<AddFeedbackModel>();

                var command = new MySqlCommand("sp_AddFeedback", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@u_id", userId);
                command.Parameters.AddWithValue("@b_id", feedbackData.BookID);
                command.Parameters.AddWithValue("@rtg", feedbackData.Rating);
                command.Parameters.AddWithValue("@cmt", feedbackData.Comment);
                command.Parameters.Add("@msg", MySqlDbType.VarChar, 50);
                command.Parameters["@msg"].Direction = ParameterDirection.Output;

                _con.Open();
                var rowsAffected = command.ExecuteNonQuery();
                var message = command.Parameters["@msg"].Value;
                _con.Close();

                result.Message = (message == DBNull.Value) ? default : message.ToString();

                if (result.Message != null)
                {
                    return result;
                }

                if (rowsAffected < 1)
                {
                    result.Message = "Unsuccessful to Add Feedback";
                    return result;
                }

                result.Status = true;
                result.Message = "Feedback Added Successfully";
                result.Data = feedbackData;
                return result;
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
                var result = new ResponseModel<List<FeedbackInfoModel>>();

                var command = new MySqlCommand("sp_GetAllFeedbacks", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@b_id", bookId);

                _con.Open();
                var dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    result.Data = new List<FeedbackInfoModel>();

                    while (dataReader.Read())
                    {
                        var feedbackData = new FeedbackInfoModel();
                        feedbackData.FeedbackID = Convert.ToInt32(dataReader["FeedbackID"]);
                        feedbackData.UserID = Convert.ToInt32(dataReader["UserID"]);
                        feedbackData.FullName = dataReader["FullName"].ToString();
                        feedbackData.BookID = Convert.ToInt32(dataReader["BookID"]);
                        feedbackData.Rating = Convert.ToInt32(dataReader["Rating"]);
                        feedbackData.Comment = dataReader["Comment"] == DBNull.Value ? default : dataReader["Comment"].ToString();
                        result.Data.Add(feedbackData);
                    }

                    _con.Close();
                    result.Status = true;
                    result.Message = $"{result.Data.Count} Feedbacks Retrived Successfully";
                    return result;
                }

                _con.Close();
                result.Message = "No Feedbacks Available";
                return result;
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
                var result = new ResponseModel<EditFeedbackModel>();

                var command = new MySqlCommand("sp_EditFeedback", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@u_id", userId);
                command.Parameters.AddWithValue("@f_id", feedbackData.FeedbackID);
                command.Parameters.AddWithValue("@rtg", feedbackData.Rating);
                command.Parameters.AddWithValue("@cmt", feedbackData.Comment);
                command.Parameters.Add("@msg", MySqlDbType.VarChar, 50);
                command.Parameters["@msg"].Direction = ParameterDirection.Output;

                _con.Open();
                var rowsAffected = command.ExecuteNonQuery();
                var message = command.Parameters["@msg"].Value;
                _con.Close();

                result.Message = (message == DBNull.Value) ? default : message.ToString();

                if (result.Message != null)
                {
                    return result;
                }

                if (rowsAffected < 1)
                {
                    result.Message = "Unsuccessful to Edit Feedback";
                    return result;
                }

                result.Status = true;
                result.Message = "Feedback Edited Successfully";
                result.Data = feedbackData;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
