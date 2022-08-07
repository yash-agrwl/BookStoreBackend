using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.OrderModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Manager
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository _repository;

        public OrderManager(IOrderRepository repository)
        {
            this._repository = repository;
        }

        public ResponseModel<PlaceOrderModel> PlaceOrder(PlaceOrderModel orderData, int userId)
        {
            try
            {
                return this._repository.PlaceOrder(orderData, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<List<OrderInfoModel>> GetAllOrders(int userId)
        {
            try
            {
                return this._repository.GetAllOrders(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
