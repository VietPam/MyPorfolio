using Microsoft.EntityFrameworkCore;
using MyPorfolio.Models.Entities;

namespace MyPorfolio.Models.Context;

public class MyPorfolioContext :DbContext
{
    public DbSet<Admin> Admins { get; set; }

    public MyPorfolioContext()
    {
        
    }
    public MyPorfolioContext(DbContextOptions<MyPorfolioContext> options) : base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Datasource=Models/mydb.db");
    }
}
