using System.ComponentModel.DataAnnotations;

namespace CommonLayer.UserModel
{
    public class RegisterModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string MobileNumber { get; set; }
    }
}
