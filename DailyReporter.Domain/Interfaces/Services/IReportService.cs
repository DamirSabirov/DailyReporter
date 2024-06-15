using DailyReporter.Domain.Dto.Report;
using DailyReporter.Domain.Entity;
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
		/// Получение всех отчётов пользователя.
		/// </summary>
		/// <param name="userId">Id пользователя</param>
		/// <returns></returns>
		Task<CollectionResult<ReportDto>> GetReportsAsync(long userId);

		/// <summary>
		/// Получение отчёта пользователя по userId.
		/// </summary>
		/// <param name="userId">Id пользователя</param>
		/// <returns></returns>
		Task<BaseResult<ReportDto>> GetReportByIdAsync(long id);
		/// <summary>
		/// Создание отчёта с базовыми параметрами.
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto);
		/// <summary>
		/// Удаление отчёта по id.
		/// </summary>
		/// <param name="id">Id отчёта</param>
		/// <returns></returns>
		Task<BaseResult<ReportDto>> DeleteReportAsync(long id);
		Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto);
	}
}
