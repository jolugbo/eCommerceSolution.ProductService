using eCommerce.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.DataAccessLayer.Context;

public class ApplicationDbContext: DbContext
{
    //private readonly IConfiguration _configuration;
    //private readonly IDbConnection _connection;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        // Initialize your Dapper database context here
        //_configuration = configuration;
        //string? connectionString = _configuration.GetConnectionString("MySqlConnection");
       // _connection = new MySqlConnection(connectionString);
    }
    //public IDbConnection DbConnection => _connection;
    public DbSet<Product> Products { get; set; } // Add DbSet for Product entity}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
