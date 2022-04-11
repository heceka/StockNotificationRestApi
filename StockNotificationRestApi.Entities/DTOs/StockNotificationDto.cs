using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StockNotificationRestApi.Entities.DTOs
{
	public class StockNotificationDto
	{
		[JsonPropertyName("productId")]
		[Required]
		[StringLength(256, MinimumLength = 4)]
		public string ProductId { get; set; }

		[JsonPropertyName("userId")]
		[Required]
		[StringLength(256)]
		public string UserId { get; set; }
	}
}
