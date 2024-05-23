using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class TesteContext : DbContext
{
	public DbSet<Member> Members { get; set; }
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite("Data Source=D:\\sqlite\\cleanarch\\MembersDB.db;Cache=Shared");
	}
}
