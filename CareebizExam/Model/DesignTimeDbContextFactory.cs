using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CareebizExam.Model
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CareebizExamDbContext>
    {
        public CareebizExamDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<CareebizExamDbContext>();
            var connectionString = configuration.GetConnectionString("CareebizExamEntities");
            builder.UseSqlServer(connectionString);
            return new CareebizExamDbContext(builder.Options);
        }
    }
}
