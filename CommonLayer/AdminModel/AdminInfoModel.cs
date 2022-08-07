namespace CommonLayer.AdminModel
{
    public class AdminInfoModel
    {
        public int AdminID { get; set; }
        public string FullName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public long MobileNumber { get; set; }
        public string ServiceType { get; } = "Admin";
    }
}
