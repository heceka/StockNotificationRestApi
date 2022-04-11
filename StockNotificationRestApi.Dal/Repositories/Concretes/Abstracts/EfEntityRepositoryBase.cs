using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StockNotificationRestApi.Dal.Contexts.EntityFramework;
using StockNotificationRestApi.Dal.Repositories.Abstracts;
using StockNotificationRestApi.Entities.Abstracts;

namespace StockNotificationRestApi.Dal.Repositories.Concretes.Abstracts
{
	public abstract class EfEntityRepositoryBase<T> : IEntityRepository<T> where T : BaseEntity, new()
	{
		#region DEFINES
		private readonly StockNotificationContext _context;
		protected internal IQueryable<T> BaseQ;
		#endregion

		#region CONSTRUCTOR
		public EfEntityRepositoryBase(StockNotificationContext context)
		{
			_context = context;
		}
		#endregion

		#region C
		public Task Add(T entity)
		{
			EntityEntry addedEntity = _context.Entry(entity);
			addedEntity.State = EntityState.Added;
			return _context.SaveChangesAsync();
		}
		#endregion

		#region R
		public Task<T> Get(Expression<Func<T, bool>> filter) => BaseQ.SingleOrDefaultAsync(filter);

		public Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null) => filter is null
			? BaseQ.ToListAsync()
			: BaseQ.Where(filter).ToListAsync();
		#endregion

		#region U
		public Task Update(T entity)
		{
			EntityEntry updatedEntity = _context.Entry(entity);
			updatedEntity.State = EntityState.Modified;
			return _context.SaveChangesAsync();
		}
		#endregion

		#region D
		public Task Delete(T entity)
		{
			EntityEntry deletedEntity = _context.Entry(entity);
			deletedEntity.State = EntityState.Deleted;
			return _context.SaveChangesAsync();
		}
		#endregion
	}
}
