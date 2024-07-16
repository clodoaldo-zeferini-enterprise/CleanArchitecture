using Domain.Entities;
using NetCore.Base.Enum;
using Domain.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;
using System;

namespace Infrastructure.SQLServer.EntityConfiguration;

public class GrupoConfiguration : IEntityTypeConfiguration<Grupo>
{
    public void Configure(EntityTypeBuilder<Grupo> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.FirstName).HasMaxLength(GrupoResources.FirstNameMaxLength).IsRequired();
        builder.Property(m => m.LastName).HasMaxLength(GrupoResources.LastNameMaxLength).IsRequired();
        builder.Property(m => m.Gender).IsRequired();

        builder.Property(m => m.Email).HasMaxLength(GrupoResources.LastNameMaxLength).IsRequired();
        builder.HasData(
            new Grupo("Clodoaldo","Zeferini", Domain.Enums.EGenero.Masculino, "clodoaldo.zeferini@gmail.com", Guid.NewGuid().ToString())
            );
        
    }
}
