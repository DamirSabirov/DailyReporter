using DailyReporter.Domain.DTO;
using DailyReporter.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Domain.Interfaces.Services
{
	/// <summary>
	/// Сервис, отвечающий за работу с доменной части отчёта (Report)
	/// </summary>
	public interface IReportService
	{
		/// <summary>
		/// Получение всех отчётов пользователя
		/// </summary>
		/// <param name="userId">Id пользователя</param>
		/// <returns></returns>
		Task<CollectionResult<ReportDto>> GetReportsAsync(long userId);
	}
}
