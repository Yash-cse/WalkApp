using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkApp.Domain.Models;

namespace WalkApp.DAL.Data
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
