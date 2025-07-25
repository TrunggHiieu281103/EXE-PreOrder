using Application.Features.User.Queries.GetAllUser;
using Application.Repository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepositoryAsync : IGenericRepositoryAsync<Users>
    {
        Task<Users> GetUserWithRolesAsync(string email, string phone);
        Task<Users> GetUserByEmailAsync(string email);
        Task<Users> GetUserByPhoneAsync(string phone);
        Task<Users> GetUserByIdWithAddressAsync(long userId);
        Task<bool> FindUserById(long userId);
        Task<Users> GetUserWithRolesByIdAsync(long userId);
        Task<IReadOnlyList<Users>> GetPagedUserResponseAsync(GetAllUserParameter parameter);
        Task<int> CountAllUsersAsync();
        Task<int> CountActiveUsersAsync();
        Task<int> CountInactiveUsersAsync();
        Task<int> CountNewUsersThisMonthAsync();
        Task<int> CountUsersByRoleAsync(string roleName);
    }
}
