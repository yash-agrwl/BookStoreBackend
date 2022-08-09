using RepositoryLayer.Interface;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using CommonLayer.UserModel;
using CommonLayer;
using MySql.Data.MySqlClient;
using Experimental.System.Messaging;

namespace RepositoryLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            this._config = configuration;
            this._connectionString = _config.GetConnectionString("BookStoreDB");
        }

        public ResponseModel<RegisterModel> Signup(RegisterModel userData)
        {
            try
            {
                var result = new ResponseModel<RegisterModel>();
                var existUser = GetUserByEmail(userData.Email);

                if (existUser == null)
                {
                    using (MySqlConnection con = new MySqlConnection(_connectionString))
                    {
                        var cmd = new MySqlCommand("sp_AddUser", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        userData.Password = EncryptPassword(userData.Password);

                        cmd.Parameters.AddWithValue("@fName", userData.FullName);
                        cmd.Parameters.AddWithValue("@email", userData.Email);
                        cmd.Parameters.AddWithValue("@pwd", userData.Password);
                        cmd.Parameters.AddWithValue("@mobileNo", userData.MobileNumber);

                        con.Open();
                        var rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected < 1)
                        {
                            result.Message = "Registration Unsuccessful";
                            return result;
                        }

                        result.Status = true;
                        result.Message = "User Registration Successful";
                        result.Data = userData;
                        return result;
                    }
                }

                result.Message = "Email Already exist";
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static string EncryptPassword(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            // Encrypt the given password string into Encrypted data.
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptData = new StringBuilder();
            // Create a new string by using the Encrypted data.
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptData.Append(encrypt[i]);
            }
            return encryptData.ToString();
        }

        public ResponseModel<UserInfoModel> Login(LoginModel userData)
        {
            try
            {
                var result = new ResponseModel<UserInfoModel>();
                var existUser = GetUserByEmail(userData.Email);
                result.Data = existUser;

                if (existUser != null)
                {
                    userData.Password = EncryptPassword(userData.Password);

                    if (userData.Password == existUser.Password)
                    {
                        result.Status = true;
                        result.Message = "Login Successful";
                        return result;
                    }
                    else
                    {
                        result.Message = "Incorrect Password";
                        return result;
                    }

                }
                else
                {
                    result.Message = "Email Not Registered";
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public UserInfoModel GetUserByEmail(string email)
        {
            var user = new UserInfoModel();
            using (MySqlConnection con = new MySqlConnection(_connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("sp_GetUserByEmail", con);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        user.UserID = Convert.ToInt32(dataReader["UserID"]);
                        user.FullName = dataReader["FullName"].ToString();
                        user.EmailID = dataReader["EmailID"].ToString();
                        user.Password = dataReader["Password"].ToString();
                        user.MobileNumber = dataReader["MobileNumber"].ToString();
                    }

                    con.Close();
                    return user;
                }

                return null;
            }

        }

        public UserInfoModel GetUserById(int id)
        {
            var user = new UserInfoModel();
            using (MySqlConnection con = new MySqlConnection(_connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("sp_GetUserById", con);
                cmd.Parameters.AddWithValue("@uid", id);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        user.UserID = Convert.ToInt32(dataReader["UserID"]);
                        user.FullName = dataReader["FullName"].ToString();
                        user.EmailID = dataReader["EmailID"].ToString();
                        user.Password = dataReader["Password"].ToString();
                        user.MobileNumber = dataReader["MobileNumber"].ToString();
                    }

                    con.Close();
                    return user;
                }

                return null;
            }
        }

        public ResponseModel<ResetPasswordModel> ResetPassword(ResetPasswordModel userData)
        {
            try
            {
                var result = new ResponseModel<ResetPasswordModel>();
                var existUser = GetUserByEmail(userData.Email);

                if (existUser != null)
                {                   
                    using (MySqlConnection con = new MySqlConnection(_connectionString))
                    {
                        var cmd = new MySqlCommand("sp_ResetPassword", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        existUser.Password = EncryptPassword(userData.Password);
                        
                        cmd.Parameters.AddWithValue("@email", existUser.EmailID);
                        cmd.Parameters.AddWithValue("@pwd", existUser.Password);

                        con.Open();
                        var rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected < 1)
                        {
                            result.Message = "Unsuccessful to Reset Password";
                            return result;
                        }

                        result.Status = true;
                        result.Message = "Password Reset Successful";
                        result.Data = userData;
                        return result;
                    }

                }

                result.Message = "Wrong Email Entered";
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string ForgotPassword(string email)
        {
            try
            {
                var existUser = GetUserByEmail(email);

                if (existUser != null)
                {
                    SendMSMQ("Link for resetting the password");
                    string linkToBeSend = ReceiveMSMQ();
                    SendMailUsingSMTP(email, linkToBeSend);
                    return "Email Sent Successfully";
                }

                return "Email Not Registered";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private static void SendMSMQ(string url)
        {
            Message message = new Message
            {
                Formatter = new BinaryMessageFormatter(),
                Body = url
            };

            MessageQueue messageQueue = QueueDetail();
            messageQueue.Label = "url link";
            messageQueue.Send(message);
        }

        private static MessageQueue QueueDetail()
        {
            MessageQueue messageQueue;
            if (MessageQueue.Exists(@".\Private$\ResetPasswordQueue"))
            {
                messageQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\ResetPasswordQueue");
            }

            return messageQueue;
        }

        private static string ReceiveMSMQ()
        {
            ////for reading from MSMQ
            var receiveQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            var receiveMsg = receiveQueue.Receive();
            receiveMsg.Formatter = new BinaryMessageFormatter();
            return receiveMsg.Body.ToString();
        }

        private void SendMailUsingSMTP(string email, string message)
        {
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(this._config["Credentials:EmailId"]),
                Subject = "Link to reset your password for BookStore Application",
                IsBodyHtml = true,
                Body = message
            };
            mailMessage.To.Add(new MailAddress(email));


            SmtpClient client = new SmtpClient
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(this._config["Credentials:EmailId"],
                                                       this._config["Credentials:EmailPassword"]),
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            client.Send(mailMessage);
        }
    }
}
