using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Domain.Dto.Report
{
	public record CreateReportDto(string Name, string Description, long UserId);
}
