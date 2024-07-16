using DailyReporter.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.DAL.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Login).IsRequired().HasMaxLength(100);
			builder.Property(x => x.Password).IsRequired().HasMaxLength(100);

			builder.HasMany<Report>(x => x.Reports)
				.WithOne(x => x.User)
				.HasForeignKey(x => x.UserId)
				.HasPrincipalKey(x => x.Id);

			builder.HasData(new List<User>()
			{
				new User()
				{
					Id = 1,
					Login = "Test",
					Password = new string('-', 20),
					CreatedAt = DateTime.UtcNow
				}
			});
		}
	}
}
