using DailyReporter.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.DAL.Configurations
{
	public class ReportConfiguration : IEntityTypeConfiguration<Report>
	{
		public void Configure(EntityTypeBuilder<Report> builder)
		{
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
			builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
		}
	}
}
