using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.UserModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UserManager(IUserRepository userRepository, IConfiguration configuration)
        {
            this._userRepository = userRepository;
            this._config = configuration;
        }

        public ResponseModel<RegisterModel> Signup(RegisterModel userData)
        {
            try
            {
                return this._userRepository.Signup(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<UserInfoModel> Login(LoginModel userData)
        {
            try
            {
                return this._userRepository.Login(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserInfoModel GetUserByEmail(string email)
        {
            try
            {
                return this._userRepository.GetUserByEmail(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserInfoModel GetUserById(int id)
        {
            try
            {
                return this._userRepository.GetUserById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<ResetPasswordModel> ResetPassword(ResetPasswordModel userData)
        {
            try
            {
                return this._userRepository.ResetPassword(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ForgotPassword(string email)
        {
            try
            {
                return this._userRepository.ForgotPassword(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GenerateToken(int userId, string emailId)
        {
            byte[] key = Encoding.UTF8.GetBytes(this._config["JwtToken:SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim("UserId", userId.ToString()),
                    new Claim ("EmailId", emailId)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
