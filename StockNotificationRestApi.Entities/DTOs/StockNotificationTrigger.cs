using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StockNotificationRestApi.Entities.DTOs
{
	public class StockNotificationTrigger
	{
		[JsonPropertyName("productId")]
		[Required]
		[StringLength(256, MinimumLength = 4)]
		public string ProductId { get; set; }

		[JsonPropertyName("productName")]
		[Required]
		public string ProductName { get; set; }
	}
}
