using Identity.Api.Models;
using Identity.Api.Services.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Services
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IDatabase _database;
        public IdentityRepository(ConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<User> UpdateUser(User user)
        {
            var created = await _database.StringSetAsync(user.Id, JsonConvert.SerializeObject(user));
            if (!created) return null;
            return await GetUser(user.Id);
        }

        public async Task<long> UpdateUserApplicationCount(string userId)
        {
            return await _database.StringIncrementAsync($"{userId}-appcnt");
        }

        public async Task<User> GetUser(string userId)
        {
            var data = await _database.StringGetAsync(userId);
            return data.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<User>(data);
        }

        public async Task<long> GetUserApplicationCount(string userId)
        {
            var data = await _database.StringGetAsync($"{userId}-appcnt");
            return data.IsNullOrEmpty ? 0 : Convert.ToInt64(data);
        }
    }
}
