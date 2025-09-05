using AuthAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Infrastructure.Contexts
{
	public class MySqlDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<UserLoginHistory> LoginHistories { get; set; }
		public MySqlDbContext(DbContextOptions options) : base(options)
		{
		}

		protected MySqlDbContext()
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}
	}
}
