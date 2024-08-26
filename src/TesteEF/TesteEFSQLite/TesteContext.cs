using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class TesteContext : DbContext
{
	public DbSet<Grupo> Grupos { get; set; }
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite("Data Source=D:\\sqlite\\cleanarch\\GruposDB.db;Cache=Shared");
	}
}
