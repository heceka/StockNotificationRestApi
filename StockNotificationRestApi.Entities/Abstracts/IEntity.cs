using System;

namespace StockNotificationRestApi.Entities.Abstracts
{
	public interface IEntity
	{
		int Id { get; set; }

		string CreatedByUser { get; set; }
		DateTime CreatedDateTime { get; set; }

		string ModifiedByUser { get; set; }
		DateTime ModifiedDateTime { get; set; }
	}
}
