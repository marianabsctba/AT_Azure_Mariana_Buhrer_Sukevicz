using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Data
{
    public class FinalContext : DbContext
    {
        public FinalContext (DbContextOptions<FinalContext> options)
            : base(options)
        {
        }

        public DbSet<Donation> Donation { get; set; }
    }
}
