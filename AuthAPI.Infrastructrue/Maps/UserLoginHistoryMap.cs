using AuthAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Infrastructure.Maps
{
	public class UserLoginHistoryMap : IEntityTypeConfiguration<UserLoginHistory>
	{
		public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
		{
			builder.ToTable("LoginHistorys");
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
		}
	}
}
