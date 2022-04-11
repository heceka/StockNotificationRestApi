using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StockNotificationRestApi.Entities.Abstracts;

namespace StockNotificationRestApi.Dal.Repositories.Abstracts
{
	public interface IEntityRepository<T> where T : BaseEntity, new()
	{
		Task Add(T entity);
		Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null);
		Task<T> Get(Expression<Func<T, bool>> filter);
		Task Update(T entity);
		Task Delete(T entity);
	}
}
