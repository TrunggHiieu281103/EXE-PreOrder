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
    public class RoleRepositoryAsync : GenericRepositoryAsync<Roles>, IRoleRepositoryAsync
    {
        private readonly DbSet<Roles> _role;


        public RoleRepositoryAsync(EXE_PreOrderContext dbContext) : base(dbContext)
        {
            _role = dbContext.Set<Roles>();
        }
    }
}
