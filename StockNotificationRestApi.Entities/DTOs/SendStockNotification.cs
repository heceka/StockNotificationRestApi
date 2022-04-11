using System.Text.Json.Serialization;

namespace StockNotificationRestApi.Entities.DTOs
{
	public class SendStockNotification
	{
		[JsonPropertyName("productId")]
		public string ProductId { get; set; }

		[JsonPropertyName("productName")]
		public string ProductName { get; set; }

		[JsonPropertyName("userId")]
		public string UserId { get; set; }
	}
}
