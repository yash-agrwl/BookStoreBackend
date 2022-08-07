namespace CommonLayer.FeedbackModel
{
    public class FeedbackInfoModel
    {
        public int FeedbackID { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; }
        public int BookID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
