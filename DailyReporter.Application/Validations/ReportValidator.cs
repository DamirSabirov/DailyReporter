using DailyReporter.Domain.Entity;
using DailyReporter.Domain.Enum;
using DailyReporter.Domain.Interfaces.Validations;
using DailyReporter.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Application.Validations
{
	public class ReportValidator : IReportValidator
	{
		public BaseResult CreateValidator(Report report, User user)
		{
			if (report != null)
			{
				return new BaseResult()
				{
					ErrorMessage = "Отчёт с таким названием уже существует",
					ErrorCode = (int)ErrorCodes.ReportAlreadyExists
				};
			}

			if (user == null)
			{
				return new BaseResult()
				{
					ErrorMessage = "Пользователь не найден",
					ErrorCode = (int)ErrorCodes.UserNotFound
				};
			}
			return new BaseResult();
		}


		public BaseResult ValidateOnNull(Report model)
		{
			if (model == null)
			{
				return new BaseResult()
				{
					ErrorMessage = "Отчёт не найден",
					ErrorCode = (int)ErrorCodes.ReportNotFound
				};
			}
			return new BaseResult();
		}
	}
}
