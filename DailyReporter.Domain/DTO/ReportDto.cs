using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Domain.DTO
{
	public record ReportDto(long id, string name, string description, string DateCreated)
	{

	}
}
