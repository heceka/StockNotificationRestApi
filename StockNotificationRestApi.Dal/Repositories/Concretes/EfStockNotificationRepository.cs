using StockNotificationRestApi.Dal.Contexts.EntityFramework;
using StockNotificationRestApi.Dal.Repositories.Abstracts;
using StockNotificationRestApi.Dal.Repositories.Concretes.Abstracts;
using StockNotificationRestApi.Entities.Concretes;

namespace StockNotificationRestApi.Dal.Repositories.Concretes
{
	public class EfStockNotificationRepository : EfEntityRepositoryBase<StockNotification>, IStockNotificationRepository
	{
		public EfStockNotificationRepository(StockNotificationContext context) : base(context)
		{
		}
	}
}
