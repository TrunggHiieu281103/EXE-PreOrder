using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepositoryAsync : GenericRepositoryAsync<Users>, IUserRepositoryAsync
    {
        private readonly DbSet<Users> _user;
   
       
        public UserRepositoryAsync(EXE_PreOrderContext dbContext) : base(dbContext)
        {
            _user = dbContext.Set<Users>();
        }
        public async Task<Users> GetUserWithRolesAsync(string email, string phone)
        {
            return await _user
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => ((u.Email == email || u.Phone == phone) && u.IsActive == true));
        }

        public async Task<Users> GetUserWithRolesByIdAsync(long userId)
        {
            return await _user
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => (u.Id == userId && u.IsActive == true));
        }

        public async Task<Users> GetUserByEmailAsync(string email)
        {
            return await _user
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => (u.Email == email && u.IsActive == true));
        }

        public async Task<Users> GetUserByPhoneAsync(string phone)
        {
            return await _user
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => (u.Phone == phone && u.IsActive == true));
        }

        public async Task<Users> GetUserByIdWithAddressAsync(long userId)
        {
            return await _user
        .Include(u => u.UserAddresses)
        .FirstOrDefaultAsync(u => u.Id == userId && u.IsActive);
        }

        public async Task<bool> FindUserById(long userId)
        {
            return await _user.AnyAsync(u => u.Id == userId && u.IsActive);
        }
    }
}
