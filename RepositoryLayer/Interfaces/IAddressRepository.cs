using CommonLayer;
using CommonLayer.AddressModel;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface IAddressRepository
    {
        ResponseModel<AddAddressModel> AddAddress(AddAddressModel address, int userId);
        ResponseModel<string> DeleteAddressById(int addressId, int userId);
        ResponseModel<List<AddressInfoModel>> GetAllAddresses(int userId);
        ResponseModel<AddressInfoModel> UpdateAddress(AddressInfoModel address, int userId);
    }
}