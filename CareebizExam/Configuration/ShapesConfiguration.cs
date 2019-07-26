using CareebizExam.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareebizExam.Configuration
{
    public class ShapesConfiguration : IEntityTypeConfiguration<Shapes>
    {
        public void Configure(EntityTypeBuilder<Shapes> builder)
        {
            builder.ToTable("Shapes");
            builder.HasKey(s => s.ShapeId);
            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(s => s.Description)
                .HasMaxLength(500);

            builder.Property(s => s.ShapeType)
                .IsRequired()
                .HasMaxLength(100);



        }
    }
}
