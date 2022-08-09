namespace CommonLayer.UserModel
{
    public class UserInfoModel
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string ServiceType { get; } = "User";
    }
}
