using Microsoft.EntityFrameworkCore;
using Rehber.API.Models.Entities;

namespace Rehber.API.Models.Context
{
    public class RehberDbContext:DbContext
    {
        public RehberDbContext(DbContextOptions<RehberDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        public DbSet<Kisi> Kisis { get; set; }
        public DbSet<IletisimBilgileri> IletisimBilgileris { get; set; }
    }
}
