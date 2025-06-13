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
    public class PaymentRepositoryAsync : GenericRepositoryAsync<Payments>, IPaymentRepositoryAsync
    {
        private readonly DbSet<Payments> _payments;
        public PaymentRepositoryAsync(EXE_PreOrderContext dbContext) : base(dbContext)
        {
            _payments = dbContext.Set<Payments>();
        }


    }
}
