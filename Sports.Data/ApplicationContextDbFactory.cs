using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sports.Data
{
    public class ApplicationContextDbFactory : IDesignTimeDbContextFactory<SportContext>
    {
        SportContext IDesignTimeDbContextFactory<SportContext>.CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<SportContext>();
            builder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SportSite2;Integrated Security=True;");
            return new SportContext(builder.Options);
        }
    }
}
