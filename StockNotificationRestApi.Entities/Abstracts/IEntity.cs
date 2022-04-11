using System;

namespace StockNotificationRestApi.Entities.Abstracts
{
	public interface IEntity
	{
		int Id { get; set; }
		DateTime CreatedDateTime { get; set; }
	}
}
