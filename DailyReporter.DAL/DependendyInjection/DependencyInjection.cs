using DailyReporter.DAL.Interceptors;
using DailyReporter.DAL.Repositories;
using DailyReporter.Domain.Entity;
using DailyReporter.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.DAL.DependendyInjection
{
	public static class DependencyInjection
	{
		public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("MSSql");

			services.AddSingleton<DateInterceptor>();
			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(connectionString);
			});
			services.InitRepositories();
		}

		private static void InitRepositories(this IServiceCollection services)
		{
			services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
			services.AddScoped<IBaseRepository<Report>, BaseRepository<Report>>();
		}
	}
}
