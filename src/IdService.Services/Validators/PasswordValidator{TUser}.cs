using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdService.Services.Validators
{
    internal sealed class PasswordValidator<TUser> : IPasswordValidator<TUser>
        where TUser : class
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var username = await manager.GetUserNameAsync(user);
            if (username.Equals(password, StringComparison.OrdinalIgnoreCase))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Username and Password can't be the same.", Code = "SameUserPass" });
            }

            if (password.Contains("password", StringComparison.OrdinalIgnoreCase))
            {
                return IdentityResult.Failed(new IdentityError { Description = "The word password is not allowed for the Password.", Code = "PasswordContainsPassword" });
            }

            return IdentityResult.Success;
        }
    }
}
