using Application.Interfaces.Repositories;
using Domain.Entities;
using Google;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserAddressRepository : GenericRepositoryAsync<UserAddresses>, IUserAddressRepositoryAsync
    {
        private readonly DbSet<UserAddresses> _userAddress;
        public UserAddressRepository(EXE_PreOrderContext dbContext) : base(dbContext)
        {
            _userAddress = dbContext.Set<UserAddresses>();
        }

        public async Task<IReadOnlyList<UserAddresses>> GetAllAddressByUserIdAsync(long userId)
        {
            return await _userAddress.Where(a => a.UserId == userId && a.IsActive).ToListAsync();
        }

        public async Task<UserAddresses> GetDefaultAddressByUserIdAsync(long userId)
        {
            return await _userAddress
                .FirstOrDefaultAsync(a => a.UserId == userId && a.IsDefault);
        }
    }
}
