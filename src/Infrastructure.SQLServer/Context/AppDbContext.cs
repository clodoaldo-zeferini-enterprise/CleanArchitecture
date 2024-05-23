using Domain.Entities;
using Infrastructure.SQLServer.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SQLServer.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=D:\\sqlite\\cleanarch\\MembersDB.db;Cache=Shared", b => b.MigrationsAssembly("API"));
    }

    public DbSet<Member> Members { get; set; }  

    protected override void OnModelCreating(ModelBuilder builder)
    {        
        builder.ApplyConfiguration(new MemberConfiguration());
    }
}
