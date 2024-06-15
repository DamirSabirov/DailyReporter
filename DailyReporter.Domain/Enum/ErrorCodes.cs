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

		InternalServerError = 20
	}
}
