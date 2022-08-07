using CommonLayer;
using CommonLayer.AdminModel;
using CommonLayer.UserModel;

namespace BusinessLayer.Interface
{
    public interface IAdminManager
    {
        string GenerateToken(int adminId, string emailId);
        AdminInfoModel GetAdminByEmail(string email);
        ResponseModel<AdminInfoModel> Login(LoginModel adminData);
    }
}