using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.Domain.Interfaces.Repositories
{
	public interface IBaseRepository<T>
	{
		IQueryable<T> GetAll();

		Task<T> CreateAsync(T entity);

		Task<T> UpdateAsync(T entity);

		Task<T> RemoveAsync(T entity);
	}
}
