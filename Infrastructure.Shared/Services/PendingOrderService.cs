using Application.Exceptions;
using Application.Features.Orders.Commands.CreateOrder;
using Application.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public class PendingOrderService : IPendingOrderService
    {
        private readonly IDatabase _db;
        private readonly int _orderExpirationMinutes;

        public PendingOrderService(IConnectionMultiplexer redis, IConfiguration configuration)
        {
            _db = redis.GetDatabase();
            _orderExpirationMinutes = configuration.GetValue<int>("Redis:PendingOrderExpirationMinutes");
        }

        public async Task StorePendingOrderAsync(string key, CreateOrderCommand order)
        {
            try
            {
                var json = JsonConvert.SerializeObject(order);
                await _db.StringSetAsync(key, json, TimeSpan.FromMinutes(_orderExpirationMinutes));
            }
            catch (Exception ex)
            {
                throw new ApiException($"Failed to store pending order for key: {key}. Error: {ex.Message}", ex);
            }
        }

        public async Task<CreateOrderCommand> GetPendingOrderAsync(string key)
        {
            try
            {
                var json = await _db.StringGetAsync(key);
                if (json.IsNullOrEmpty) return null;

                return JsonConvert.DeserializeObject<CreateOrderCommand>(json);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Failed to get pending order for key: {key}. Error: {ex.Message}", ex);
            }
        }

        public async Task RemovePendingOrderAsync(string key)
        {
            try
            {
                await _db.KeyDeleteAsync(key);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Failed to delete pending order for key: {key}. Error: {ex.Message}", ex);
            }
        }
    }
}
