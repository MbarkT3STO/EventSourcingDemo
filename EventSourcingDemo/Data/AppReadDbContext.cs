using System;
using EventSourcingDemo.Data.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingDemo.Data
{
	public class AppReadDbContext : DbContext
	{
		public AppReadDbContext(DbContextOptions<AppReadDbContext> options) : base(options)
		{
		}
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductReadModel>().HasKey(x => x.Id);
			modelBuilder.Entity<ProductReadModel>().Property(x => x.Name).HasMaxLength(100);
		}
		
		public DbSet<ProductReadModel> Products { get; set; }
	}
}