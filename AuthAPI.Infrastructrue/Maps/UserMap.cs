using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AuthAPI.Infrastructure.Maps
{
	public class UserMap : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");
			builder.HasKey(p => p.Id);
			builder.OwnsOne(p => p.PhoneNumber, p =>
			{
				p.Property(p => p.RegionCode)
				.HasMaxLength(3)
				.IsUnicode(false);
				p.Property(p => p.Number)
				.HasMaxLength(12)
				.IsUnicode(false);
			});
			builder.HasOne(p => p.AccessFail)
				.WithOne(p => p.User)
				.HasForeignKey<UserAccessFail>(p=>p.UserId);
			builder.Property(p => p.PasswordHash).IsUnicode(false);
		}
	}
}
