using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Goal.Demo2.Domain.Aggregates.People;

namespace Goal.Demo2.Infra.Data.Configurations
{
    internal sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired();

            builder.OwnsOne(p => p.Name, name =>
            {
                name.Property(p => p.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                name.Property(p => p.LastName)
                    .HasMaxLength(50)
                    .IsRequired();
            });

            builder.HasOne(p => p.Cpf)
                .WithOne(p => p.Person)
                .HasForeignKey<Document>(e => e.Id);
        }
    }
}
