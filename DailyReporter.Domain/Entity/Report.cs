using DailyReporter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Domain.Entity
{
	public class Report : IEntityId<long>, IAuditable
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public User User { get; set; }
		public long UserId { get; set; }
		public DateTime CreatedAt { get; set; }
		public long CreatedBy { get; set; }
		public DateTime UpdatedAt { get; set; }
		public long UpdatedBy { get; set; }
	}

}
