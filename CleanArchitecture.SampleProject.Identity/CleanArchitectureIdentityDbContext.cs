using CleanArchitecture.SampleProject.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.SampleProject.Identity
{
    public class CleanArchitectureIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public CleanArchitectureIdentityDbContext(DbContextOptions<CleanArchitectureIdentityDbContext> options) : base(options)
        {
        }
    }
}
