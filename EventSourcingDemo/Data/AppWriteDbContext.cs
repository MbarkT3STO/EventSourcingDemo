using System;
using EventSourcingDemo.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingDemo.Data
{
	public class AppWriteDbContext : DbContext
	{
		public AppWriteDbContext(DbContextOptions<AppWriteDbContext> options) : base(options)
		{
		}
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>().HasKey(x => x.Id);
			modelBuilder.Entity<Product>().Property(x => x.Name).HasMaxLength(100);
		}
		
		public DbSet<Product> Products { get; set; }
	}
}