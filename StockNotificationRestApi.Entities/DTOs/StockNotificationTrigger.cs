using System.ComponentModel.DataAnnotations;

namespace StockNotificationRestApi.Entities.DTOs
{
	public class StockNotificationTrigger
	{
		[Required]
		[StringLength(256, MinimumLength = 4)]
		public string ProductId { get; set; }
	}
}
