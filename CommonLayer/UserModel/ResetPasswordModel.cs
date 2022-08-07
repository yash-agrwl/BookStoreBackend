using System.ComponentModel.DataAnnotations;

namespace CommonLayer.UserModel
{
    public class ResetPasswordModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
