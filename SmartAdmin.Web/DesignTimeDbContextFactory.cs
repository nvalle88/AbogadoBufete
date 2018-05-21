using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SmartAdmin.Web.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.Web
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BufeteDbContext>
    {
        public BufeteDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<BufeteDbContext>();
            var connectionString = configuration.GetConnectionString("BufeteConnection");
            builder.UseSqlServer(connectionString);
            return new BufeteDbContext(builder.Options);
        }
    }
}
