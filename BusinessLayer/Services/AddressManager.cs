using BusinessLayer.Interface;
using CommonLayer;
using CommonLayer.AddressModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Manager
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepository _repository;

        public AddressManager(IAddressRepository repository)
        {
            this._repository = repository;
        }

        public ResponseModel<AddAddressModel> AddAddress(AddAddressModel address, int userId)
        {
            try
            {
                return this._repository.AddAddress(address, userId);
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
                return this._repository.UpdateAddress(address, userId);
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
                return this._repository.GetAllAddresses(userId);
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
                return this._repository.DeleteAddressById(addressId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
