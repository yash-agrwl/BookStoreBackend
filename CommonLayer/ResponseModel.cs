using System.ComponentModel;

namespace CommonLayer
{
    public class ResponseModel<T>
    {
        [DefaultValue(false)]
        public bool Status { get; set; }

        [DefaultValue(null)]
        public string Message { get; set; }

        [DefaultValue(null)]
        public T Data { get; set; }
    }
}
