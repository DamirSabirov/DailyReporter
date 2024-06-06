using DailyReporter.Application.Resources;
using DailyReporter.Domain.DTO;
using DailyReporter.Domain.Entity;
using DailyReporter.Domain.Enum;
using DailyReporter.Domain.Interfaces.Repositories;
using DailyReporter.Domain.Interfaces.Services;
using DailyReporter.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Application.Services
{
	public class ReportService : IReportService
	{
		private readonly IBaseRepository<Report> _reportRepository;
		private readonly ILogger _logger;

		public ReportService(IBaseRepository<Report> reportRepository, ILogger logger)
		{
			_reportRepository = reportRepository;
			_logger = logger;
		}

		public async Task<CollectionResult<ReportDto>> GetReportsAsync(long userId)
		{
			ReportDto[] reports;

			try
			{
				reports = await _reportRepository.GetAll()
					.Where(x => x.UserId == userId)
					.Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
					.ToArrayAsync();
			}
			catch (Exception ex)
			{
				_logger.Error(ex, ex.Message);
				return new CollectionResult<ReportDto>()
				{
					ErrorMessage = "Внутренняя ошибка сервера",
					ErrorCode = (int)ErrorCodes.InternalServerError
				};
			}

			if (!reports.Any())
			{
				_logger.Warning("Отчёты не найдены", reports.Length);

				return new CollectionResult<ReportDto>()
				{
					ErrorMessage = "Записи не найдены",
					ErrorCode = (int)ErrorCodes.ReportsNotFound
				};
			}

			return new CollectionResult<ReportDto>()
			{
				Data = reports,
				Count = reports.Length
			};

		}
	}
}
