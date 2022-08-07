using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommonLayer.FeedbackModel
{
    public class AddFeedbackModel
    {
        [Required]
        public int BookID { get; set; }
        [Required]
        public int Rating { get; set; }
        [DefaultValue(null)]
        public string Comment { get; set; }
    }
}
