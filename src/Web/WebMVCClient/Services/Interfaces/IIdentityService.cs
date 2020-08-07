using System.Threading.Tasks;
using WebMVCClient.ViewModels;

namespace WebMVCClient.Services.Interfaces
{
    public interface IIdentityService
    {
        Task UpdateUserAsync(User user);
        Task<User> GetUserAsync(string userId);
        Task<long> GetUserApplicationCountAsync(string userId);
    }
}
