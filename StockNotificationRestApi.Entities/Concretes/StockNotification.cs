using StockNotificationRestApi.Entities.Abstracts;

namespace StockNotificationRestApi.Entities.Concretes
{
	public class StockNotification : BaseEntity
	{
		public string ProductId { get; set; }
		public string UserId { get; set; }
	}
}
