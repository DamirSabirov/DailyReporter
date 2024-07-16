using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Domain.Enum
{
	public enum ErrorCodes
	{
		ReportsNotFound = 0,
		ReportNotFound = 1,
		ReportAlreadyExists = 2,

		UserNotFound = 11,
		UserAlreadyExists = 12,

		PasswordsNotEqual = 21,


		InternalServerError = 30
	}
}
