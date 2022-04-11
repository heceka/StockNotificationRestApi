using System.ComponentModel.DataAnnotations;

namespace StockNotificationRestApi.Entities.DTOs
{
	public class StockNotificationDto : StockNotificationTrigger
	{
		[Required]
		[StringLength(256)]
		public string UserId { get; set; }
	}
}
