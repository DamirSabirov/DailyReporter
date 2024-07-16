using DailyReporter.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.DAL.Interceptors
{
	public class DateInterceptor : SaveChangesInterceptor
	{
		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			var dbContext = eventData.Context;
			if (dbContext == null)
			{
				return base.SavingChanges(eventData, result);
			}

			var entries = dbContext.ChangeTracker.Entries<IAuditable>()
				.Where(x => x.State == EntityState.Added || x.State == EntityState.Modified)
				.ToList();
			foreach (var entry in entries)
			{
				if (entry.State == EntityState.Added)
				{
					entry.Property (x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
				}

				if (entry.State == EntityState.Modified)
				{
					entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
				}
			}

			return base.SavingChanges(eventData, result);
		}
	}
}
