using Microsoft.EntityFrameworkCore;
using MySqlConnector;


public class MySQLContext : DbContext
{
	public DbSet<Domain.Entities.Grupo> Grupos { get; set; }
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var sqlConnection = "Server=localhost;Port=33060;DataBase=GruposDB;Uid=root;Pwd=admin123";
        optionsBuilder.UseMySql(sqlConnection,
            ServerVersion.AutoDetect(sqlConnection));
	}
}
