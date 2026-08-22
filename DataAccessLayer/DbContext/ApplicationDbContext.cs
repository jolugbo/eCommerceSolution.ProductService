using BusinessLogicLayer.Entities;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccessLayer.DbContext;

public class ApplicationDbContext
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _connection;
    //public DbSet<Product> Products { get; set; } // Add DbSet for Product entity}
    public ApplicationDbContext(IConfiguration configuration)
    {
        // Initialize your Dapper database context here
        _configuration = configuration;
        string? connectionString = _configuration.GetConnectionString("MySqlConnection");
       // _connection = new MySqlConnection(connectionString);
    }
    public IDbConnection DbConnection => _connection;

    public void Dispose()
    {
        _connection?.Dispose();
    }
}
