using CommonLayer;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using RepositoryLayer.Interface;
using CommonLayer.UserModel;
using CommonLayer.AdminModel;

namespace RepositoryLayer.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public AdminRepository(IConfiguration configuration)
        {
            this._config = configuration;
            this._connectionString = _config.GetConnectionString("BookStoreDB");
        }

        public ResponseModel<AdminInfoModel> Login(LoginModel adminData)
        {
            try
            {
                var result = new ResponseModel<AdminInfoModel>();
                var existUser = GetAdminByEmail(adminData.Email);
                result.Data = existUser;

                if (existUser != null)
                {
                    adminData.Password = EncryptPassword(adminData.Password);

                    if (adminData.Password == existUser.Password)
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

        public AdminInfoModel GetAdminByEmail(string email)
        {
            var admin = new AdminInfoModel();
            using (var con = new MySqlConnection(_connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("sp_GetAdminByEmail", con);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        admin.AdminID = Convert.ToInt32(dataReader["AdminID"]);
                        admin.FullName = dataReader["FullName"].ToString();
                        admin.EmailID = dataReader["EmailID"].ToString();
                        admin.Password = dataReader["Password"].ToString();
                        admin.MobileNumber = Convert.ToInt64(dataReader["MobileNumber"]);
                    }

                    con.Close();
                    return admin;
                }

                return null;
            }

        }
    }
}
