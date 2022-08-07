using CommonLayer;
using CommonLayer.OrderModel;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace RepositoryLayer.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        private readonly MySqlConnection _con = new MySqlConnection();
        private readonly IBookRepository _bookRepo;

        public OrderRepository(IConfiguration configuration, IBookRepository bookRepo)
        {
            this._config = configuration;
            this._connectionString = _config.GetConnectionString("BookStoreDB");
            this._con.ConnectionString = this._connectionString;
            this._bookRepo = bookRepo;
        }

        public ResponseModel<PlaceOrderModel> PlaceOrder(PlaceOrderModel orderData, int userId)
        {
            try
            {
                var result = new ResponseModel<PlaceOrderModel>();

                var command = new MySqlCommand("sp_PlaceOrder", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@u_id", userId);
                command.Parameters.AddWithValue("@c_id", orderData.CartID);
                command.Parameters.AddWithValue("@add_id", orderData.AddressID);
                //command.Parameters.AddWithValue("@date", DateTime.Now.ToString("MMMM dd, yyyy"));
                command.Parameters.Add("@msg", MySqlDbType.Text);
                command.Parameters["@msg"].Direction = ParameterDirection.Output;

                _con.Open();
                var rowsAffected = command.ExecuteNonQuery();
                var message = command.Parameters["@msg"].Value;
                _con.Close();

                result.Message = (message == DBNull.Value) ? default : message.ToString();

                if (result.Message != null)
                {
                    return result;
                }

                if (rowsAffected < 1)
                {
                    result.Message = "Unsuccessful to Place Order";
                    return result;
                }

                result.Status = true;
                result.Message = "Order Placed Successfully";
                result.Data = orderData;
                return result;
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
                var result = new ResponseModel<List<OrderInfoModel>>();

                var command = new MySqlCommand("sp_GetAllOrders", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@u_id", userId);

                _con.Open();
                var dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    result.Data = new List<OrderInfoModel>();

                    while (dataReader.Read())
                    {
                        var orderData = new OrderInfoModel();
                        orderData.OrderID = Convert.ToInt32(dataReader["OrderID"]);
                        orderData.UserID = Convert.ToInt32(dataReader["UserID"]);
                        orderData.BookID = Convert.ToInt32(dataReader["BookID"]);
                        orderData.AddressID = Convert.ToInt32(dataReader["AddressID"]);
                        orderData.OrderQty = Convert.ToInt32(dataReader["OrderQty"]);
                        orderData.TotalPrice = (float) dataReader["TotalPrice"];
                        orderData.OrderDate = Convert.ToDateTime(dataReader["OrderDate"]).ToString("MMMM dd, yyyy");
                        var book = _bookRepo.GetBookById(orderData.BookID);
                        orderData.BookData = book.Data;
                        result.Data.Add(orderData);
                    }

                    _con.Close();
                    result.Status = true;
                    result.Message = $"{result.Data.Count} Orders Retrived Successfully";
                    return result;
                }

                _con.Close();
                result.Message = "No Orders Available";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
