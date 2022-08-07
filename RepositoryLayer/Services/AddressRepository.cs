using CommonLayer;
using CommonLayer.AddressModel;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace RepositoryLayer.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        private readonly MySqlConnection _con = new MySqlConnection();

        public AddressRepository(IConfiguration configuration)
        {
            this._config = configuration;
            this._connectionString = _config.GetConnectionString("BookStoreDB");
            this._con.ConnectionString = this._connectionString;
        }

        public ResponseModel<AddAddressModel> AddAddress(AddAddressModel address, int userId)
        {
            try
            {
                var result = new ResponseModel<AddAddressModel>();

                var command = new MySqlCommand("sp_AddAddress", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@address", address.Address);
                command.Parameters.AddWithValue("@city", address.City);
                command.Parameters.AddWithValue("@state", address.State);
                command.Parameters.AddWithValue("@t_id", address.TypeID);
                command.Parameters.AddWithValue("@u_id", userId);

                _con.Open();
                var rowsAffected = command.ExecuteNonQuery();
                _con.Close();

                if (rowsAffected < 1)
                {
                    result.Message = "Unsuccessful to Add Address";
                    return result;
                }

                result.Status = true;
                result.Message = "Address Successfully Added";
                result.Data = address;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<AddressInfoModel> UpdateAddress(AddressInfoModel address, int userId)
        {
            try
            {
                var result = new ResponseModel<AddressInfoModel>();

                var command = new MySqlCommand("sp_UpdateAddress", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@add_id", address.AddressID);
                command.Parameters.AddWithValue("@address", address.Address);
                command.Parameters.AddWithValue("@city", address.City);
                command.Parameters.AddWithValue("@state", address.State);
                command.Parameters.AddWithValue("@u_id", userId);

                _con.Open();
                var rowsAffected = command.ExecuteNonQuery();
                _con.Close();

                if (rowsAffected < 1)
                {
                    result.Message = "Unsuccessful to Update Address";
                    return result;
                }

                result.Status = true;
                result.Message = "Address Updated Successfully";
                result.Data = address;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<List<AddressInfoModel>> GetAllAddresses(int userId)
        {
            try
            {
                var result = new ResponseModel<List<AddressInfoModel>>();

                var command = new MySqlCommand("sp_GetAllAddresses", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@u_id", userId);

                _con.Open();
                var dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    result.Data = new List<AddressInfoModel>();

                    while (dataReader.Read())
                    {
                        var addressData = new AddressInfoModel();
                        addressData.AddressID = Convert.ToInt32(dataReader["AddressID"]);
                        addressData.Address = dataReader["Address"].ToString();
                        addressData.City = dataReader["City"].ToString();
                        addressData.State = dataReader["State"].ToString();
                        addressData.Type = dataReader["Type"].ToString();
                        result.Data.Add(addressData);
                    }

                    _con.Close();
                    result.Status = true;
                    result.Message = $"{result.Data.Count} Addresses Retrived Successfully";

                    return result;
                }

                _con.Close();
                result.Message = "No Address Available";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<string> DeleteAddressById(int addressId, int userId)
        {
            try
            {
                var result = new ResponseModel<string>();

                var command = new MySqlCommand("sp_DeleteAddressById", _con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@add_id", addressId);
                command.Parameters.AddWithValue("@u_id", userId);

                _con.Open();
                var rowsAffected = command.ExecuteNonQuery();
                _con.Close();

                if (rowsAffected < 1)
                {
                    result.Message = "Unsuccessful To Delete Address";
                    return result;
                }

                result.Status = true;
                result.Message = "Address Successfully Deleted";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
