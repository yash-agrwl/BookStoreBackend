using CommonLayer;
using CommonLayer.UserModel;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        string ForgotPassword(string email);
        UserInfoModel GetUserByEmail(string email);
        UserInfoModel GetUserById(int id);
        ResponseModel<UserInfoModel> Login(LoginModel userData);
        ResponseModel<ResetPasswordModel> ResetPassword(ResetPasswordModel userData);
        ResponseModel<RegisterModel> Signup(RegisterModel userData);
    }
}