using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sports.DomainModel
{
    public class ApplicationContextDbFactory : IDesignTimeDbContextFactory<SportContext>
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        SportContext IDesignTimeDbContextFactory<SportContext>.CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<SportContext>();
            builder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SportSiteAuth;Integrated Security=True;");
            return new SportContext(builder.Options);
        }
    }
}
