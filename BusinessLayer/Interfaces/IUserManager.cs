using CommonLayer;
using CommonLayer.UserModel;

namespace BusinessLayer.Interface
{
    public interface IUserManager
    {
        ResponseModel<RegisterModel> Signup(RegisterModel userData);
        ResponseModel<UserInfoModel> Login(LoginModel userData);
        UserInfoModel GetUserByEmail(string email);
        UserInfoModel GetUserById(int id);
        ResponseModel<ResetPasswordModel> ResetPassword(ResetPasswordModel userData);
        string ForgotPassword(string email);
        string GenerateToken(int userId, string emailId);
    }
}