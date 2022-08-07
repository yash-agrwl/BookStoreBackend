using CommonLayer;
using CommonLayer.AdminModel;
using CommonLayer.UserModel;

namespace RepositoryLayer.Interface
{
    public interface IAdminRepository
    {
        AdminInfoModel GetAdminByEmail(string email);
        ResponseModel<AdminInfoModel> Login(LoginModel adminData);
    }
}