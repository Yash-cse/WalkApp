using Microsoft.EntityFrameworkCore;
using WalkApp.Domain.WalkApp.Domain.Models;

namespace WalkApp.DAL.WalkApp.DAL.Data
{
    public class WalkAppDbContext : DbContext
    {
        public WalkAppDbContext(DbContextOptions<WalkAppDbContext> options) : base(options)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walks> Walks { get; set; }

    }
}
