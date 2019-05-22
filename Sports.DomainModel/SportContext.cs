using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sports.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sports.DomainModel
{
    public class SportContext : IdentityDbContext<ApplicationUser> {
        public SportContext(DbContextOptions<SportContext> option) : base(option)
        {
        }
        public DbSet<Test> Tests { get; set; }
        public DbSet<User> mainUsers { get; set; }
        public DbSet<Athlete> Athletes { get; set; }
    }
    
}
