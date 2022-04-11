using System;

namespace StockNotificationRestApi.Entities.Abstracts
{
	public abstract class BaseEntity : IEntity
	{
		public int Id { get; set; }

		public string CreatedByUser { get; set; }
		public DateTime CreatedDateTime { get; set; } = DateTime.Now;

		public string ModifiedByUser { get; set; }
		public DateTime ModifiedDateTime { get; set; } = DateTime.Now;
	}
}
