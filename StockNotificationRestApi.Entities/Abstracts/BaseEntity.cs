using System;

namespace StockNotificationRestApi.Entities.Abstracts
{
	public abstract class BaseEntity : IEntity
	{
		public int Id { get; set; }
		public DateTime CreatedDateTime { get; set; } = DateTime.Now;
	}
}
