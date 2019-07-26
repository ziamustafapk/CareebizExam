using CareebizExam.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CareebizExam.Model
{
    public class CareebizExamDbContext : DbContext
    {
        public CareebizExamDbContext(DbContextOptions<CareebizExamDbContext> options)
            : base(options) { }

        #region DbSet Properties
        public DbSet<Shapes> Shapes { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Configuration

            modelBuilder.ApplyConfiguration(new ShapesConfiguration());
            
            #endregion

        }
    }
}
