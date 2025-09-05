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
	public class UserAccessFailMap : IEntityTypeConfiguration<UserAccessFail>
	{
		public void Configure(EntityTypeBuilder<UserAccessFail> builder)
		{
			builder.ToTable("AccessFails");
			builder.HasKey(x => x.Id);
			builder.Property(p => p.LockOut);
		}
	}
}
