using CommonLayer;
using CommonLayer.OrderModel;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface IOrderRepository
    {
        ResponseModel<List<OrderInfoModel>> GetAllOrders(int userId);
        ResponseModel<PlaceOrderModel> PlaceOrder(PlaceOrderModel orderData, int userId);
    }
}