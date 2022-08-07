using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.AdminModel;
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
    public class AdminManager : IAdminManager
    {
        private readonly IAdminRepository _repository;
        private readonly IConfiguration _config;

        public AdminManager(IAdminRepository repository, IConfiguration configuration)
        {
            this._repository = repository;
            this._config = configuration;
        }

        public ResponseModel<AdminInfoModel> Login(LoginModel adminData)
        {
            try
            {
                return this._repository.Login(adminData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AdminInfoModel GetAdminByEmail(string email)
        {
            try
            {
                return this._repository.GetAdminByEmail(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GenerateToken(int adminId, string emailId)
        {
            byte[] key = Encoding.UTF8.GetBytes(this._config["JwtToken:SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("AdminId", adminId.ToString()),
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
