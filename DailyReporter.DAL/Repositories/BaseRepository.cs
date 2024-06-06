using DailyReporter.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReporter.DAL.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		private readonly ApplicationDbContext _dbContext;

		public BaseRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Task<T> CreateAsync(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException("Entity is null");

			_dbContext.Add(entity);
			_dbContext.SaveChanges();

			return Task.FromResult(entity);
		}

		public IQueryable<T> GetAll()
		{
			return _dbContext.Set<T>();
		}

		public Task<T> RemoveAsync(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException("Entity is null");

			_dbContext.Remove(entity);
			_dbContext.SaveChanges();

			return Task.FromResult(entity);
		}

		public Task<T> UpdateAsync(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException("Entity is null");

			_dbContext.Update(entity);
			_dbContext.SaveChanges();

			return Task.FromResult(entity);
		}
	}
}
