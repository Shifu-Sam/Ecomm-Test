using Ecomm_Database_Class.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.Repository.IRepository
{
    public interface IAddressRepo
    {
        // Create a new shipping address
        Task AddAddressAsync(Address address);

        // Read: Get a shipping address by AddressId
        Task<Address?> GetAddressByIdAsync(int addressId);

        // Read: Get all shipping addresses by UserId
        Task<List<Address>> GetAddressesByUserIdAsync(int userId);

        // Update an existing shipping address
        Task UpdateAddressAsync(Address address);

        // Delete a shipping address by AddressId
        Task DeleteAddressAsync(int addressId);

        // Add DetachEntity method
        void DetachEntity(Address address);
    }
}
