using System;
using IdService.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace IdService.Data.Model.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public UserStatus Status { get; set; }

        public string? Description { get; set; }
    }
}
