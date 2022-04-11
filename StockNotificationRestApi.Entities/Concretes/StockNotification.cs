using StockNotificationRestApi.Entities.Abstracts;
using StockNotificationRestApi.Entities.Enums;

namespace StockNotificationRestApi.Entities.Concretes
{
	public class StockNotification : BaseEntity
	{
		public string ProductId { get; set; }
		public string UserId { get; set; }
		public string UserEmail { get; set; }
		public NotificationType NotificationType { get; set; }
	}
}
