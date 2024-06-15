using DailyReporter.Domain.Entity;
using DailyReporter.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Domain.Interfaces.Validations
{
	public interface IReportValidator : IBaseValidator<Report>
	{
		/// <summary>
		/// Проверяется наличие отчёта, если отчёт с переданным названием есть в БД, то создать такой отчёт не получится
		/// Проверяется пользователь, если пользователь с UserId не найден, то не создавать отчёт
		/// </summary>
		/// <param name="report">Отчёт</param>
		/// <param name="user">Ползователь</param>
		/// <returns></returns>
		BaseResult CreateValidator(Report report, User user);
	}
}
