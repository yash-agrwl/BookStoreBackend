using CommonLayer.BookModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.OrderModel
{
    public class OrderInfoModel
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public BookInfoModel BookData { get; set; }
        public int AddressID { get; set; }
        public int OrderQty { get; set; }
        public float TotalPrice { get; set; }
        public string OrderDate { get; set; }
    }
}
