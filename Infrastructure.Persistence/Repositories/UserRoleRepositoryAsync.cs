using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRoleRepositoryAsync : GenericRepositoryAsync<UserRoles>, IUserRoleRepositoryAsync
    {
        private readonly DbSet<UserRoles> _userRole;


        public UserRoleRepositoryAsync(EXE_PreOrderContext dbContext) : base(dbContext)
        {
            _userRole = dbContext.Set<UserRoles>();
        }
    }
}
