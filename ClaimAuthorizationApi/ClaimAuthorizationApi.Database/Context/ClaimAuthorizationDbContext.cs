using ClaimAuthorizationApi.Model.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClaimAuthorizationApi.Database.Context
{
    public class ClaimAuthorizationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ClaimAuthorizationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
