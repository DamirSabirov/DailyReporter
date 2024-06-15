using DailyReporter.Application.Mapping;
using DailyReporter.Application.Services;
using DailyReporter.Application.Validations;
using DailyReporter.Application.Validations.FluentValidations.Report;
using DailyReporter.Domain.Dto.Report;
using DailyReporter.Domain.Interfaces.Services;
using DailyReporter.Domain.Interfaces.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Application.DependencyInjection
{
	public static class DependencyInjection
	{
		public static void AddApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(ReportMapping));

			InitServices(services);
		}

		private static void InitServices(this IServiceCollection services)
		{
			services.AddScoped<IReportValidator, ReportValidator>();
			services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
			services.AddScoped<IValidator<UpdateReportDto>, UpdateReportValidator>();

			services.AddScoped<IReportService, ReportService>();
		}

	}
}
