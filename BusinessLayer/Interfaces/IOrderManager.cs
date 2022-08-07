using CommonLayer;
using CommonLayer.OrderModel;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface IOrderManager
    {
        ResponseModel<List<OrderInfoModel>> GetAllOrders(int userId);
        ResponseModel<PlaceOrderModel> PlaceOrder(PlaceOrderModel orderData, int userId);
    }
}