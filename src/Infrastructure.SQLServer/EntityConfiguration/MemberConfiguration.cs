using Domain.Entities;
using Domain.Enums;
using Domain.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace Infrastructure.SQLServer.EntityConfiguration;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.FirstName).HasMaxLength(MemberResources.FirstNameMaxLength).IsRequired();
        builder.Property(m => m.LastName).HasMaxLength(MemberResources.LastNameMaxLength).IsRequired();
        builder.Property(m => m.Gender).IsRequired();

        builder.Property(m => m.Email).HasMaxLength(MemberResources.LastNameMaxLength).IsRequired();
        builder.HasData(
            new Member("Clodoaldo","Zeferini", EGenero.Masculino, "clodoaldo.zeferini@gmail.com", Guid.NewGuid().ToString())
            );
        
    }
}
