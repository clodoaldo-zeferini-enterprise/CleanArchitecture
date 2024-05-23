using Microsoft.EntityFrameworkCore;


public class PostgreSQLContext : DbContext
{
	public DbSet<Domain.Entities.Member> Members { get; set; }
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql("Pooling=true;Host=localhost;Port=54320;Database=MembersDB;User Id=root;Password=admin123");
	}
}
