using System.Threading.Tasks;
using IdService.Core.Enums;
using IdService.Data.Model.Identity;
using Microsoft.AspNetCore.Identity;

namespace IdService.Services.Identity
{
    internal sealed class UserConfirmation<TUser> : IUserConfirmation<TUser>
        where TUser : ApplicationUser
    {
        public async Task<bool> IsConfirmedAsync(UserManager<TUser> manager, TUser user)
        {
            return await Task.FromResult(user.Status is UserStatus.Active or UserStatus.Pending);
        }
    }
}
