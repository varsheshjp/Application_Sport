using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sports.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sports.Data
{
    public class SportContext : DbContext {
        public SportContext(DbContextOptions<SportContext> option) : base(option)
        {
        }
        public DbSet<Test> Tests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Athlete> Athletes { get; set; }
    }
    
}
