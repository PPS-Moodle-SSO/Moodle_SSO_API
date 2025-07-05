using Microsoft.EntityFrameworkCore;
using Moodle_SSO_API.Models;

namespace Moodle_SSO_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<BankCard> bank_card { get; set; }
        public DbSet<Account> account { get; set; }


    }
}
