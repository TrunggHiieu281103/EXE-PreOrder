using Application.Repository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IUserAddressRepositoryAsync : IGenericRepositoryAsync<UserAddresses>
    {
        Task<UserAddresses> GetDefaultAddressByUserIdAsync(long userId);
        Task<IReadOnlyList<UserAddresses>> GetAllAddressByUserIdAsync(long userId);

    }
}
