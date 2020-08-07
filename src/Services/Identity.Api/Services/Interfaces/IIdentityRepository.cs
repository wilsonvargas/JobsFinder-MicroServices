using Identity.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Services.Interfaces
{
    public interface IIdentityRepository
    {
        Task<User> UpdateUser(User user);
        Task<User> GetUser(string userId);
        Task<long> UpdateUserApplicationCount(string userId);
        Task<long> GetUserApplicationCount(string userId);
    }
}
