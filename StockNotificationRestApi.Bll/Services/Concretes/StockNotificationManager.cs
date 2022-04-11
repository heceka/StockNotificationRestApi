#region USINGS
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StockNotificationRestApi.Bll.Services.Abstracts;
using StockNotificationRestApi.Core.Resources;
using StockNotificationRestApi.Core.Utilities.Results.Abstracts;
using StockNotificationRestApi.Core.Utilities.Results.Concretes;
using StockNotificationRestApi.Dal.Repositories.Abstracts;
using StockNotificationRestApi.Entities.Concretes;
using StockNotificationRestApi.Entities.DTOs;
#endregion

namespace StockNotificationRestApi.Bll.Services.Concretes
{
	public class StockNotificationManager : IStockNotificationService
	{
		#region DEFINES
		private readonly IStockNotificationRepository _repository;
		private readonly ILogger<StockNotificationManager> _log;
		#endregion

		#region CONSTRUCTOR
		public StockNotificationManager(IStockNotificationRepository repository, ILogger<StockNotificationManager> log)
		{
			_repository = repository;
			_log = log;
		}
		#endregion

		#region GET BY PRODUCT ID AND USER ID
		public async Task<IDataResult<StockNotification>> GetByProductIdAndUserId(string productId, string userId)
		{
			StockNotification stockNotification = await _repository.Get(x => x.ProductId == productId && x.UserId == userId);
			if( stockNotification is null )
			{
				return new ErrorDataResult<StockNotification>(StaticMessages.StockNotificationNotFound);
			}
			return new SuccessDataResult<StockNotification>(stockNotification);
		}

		#endregion

		#region GET ALL BY PRODUCT ID
		public async Task<IDataResult<List<StockNotification>>> GetAllByProductId(string productId)
		{
			#region RULES
			if( String.IsNullOrEmpty(productId) )
			{
				return new ErrorDataResult<List<StockNotification>>(StaticMessages.ProductIdNullOrEmpty);
			}
			#endregion

			List<StockNotification> stockNotifications = await _repository.GetAll(x => x.ProductId == productId);
			if( stockNotifications is null )
			{
				return new ErrorDataResult<List<StockNotification>>(StaticMessages.StockNotificationNotFound);
			}
			return new SuccessDataResult<List<StockNotification>>(stockNotifications);
		}
		#endregion

		#region ADD
		public async Task<IResult> Add(StockNotificationDto model)
		{
			#region RULES
			if( String.IsNullOrEmpty(model.ProductId) )
			{
				return new ErrorResult(StaticMessages.ProductIdNullOrEmpty);
			}
			else if( String.IsNullOrEmpty(model.UserId) )
			{
				return new ErrorResult(StaticMessages.UserIdNullOrEmpty);
			}

			IDataResult<StockNotification> dataResult = await GetByProductIdAndUserId(model.ProductId, model.UserId);
			if( !dataResult.Success )
			{
				return new ErrorResult(StaticMessages.StockNotificationAlreadyExists);
			}
			#endregion

			StockNotification newStockNotification = new StockNotification
			{
				ProductId = model.ProductId,
				UserId = model.UserId
			};
			await _repository.Add(newStockNotification);
			return new SuccessResult();
		}
		#endregion

		#region REMOVE
		public async Task<IResult> Remove(StockNotificationDto model)
		{
			IDataResult<StockNotification> dataResult = await GetByProductIdAndUserId(model.ProductId, model.UserId);
			if( dataResult.Success )
			{
				await _repository.Delete(dataResult.Data);
				return new SuccessResult();
			}
			return dataResult;
			//TODO return new ErrorResult(dataResult.MessageResourceName);
		}
		#endregion

		#region REMOVE ALL
		public async Task<IResult> RemoveAll(List<StockNotification> stockNotifications)
		{
			for( int i = 0; i < stockNotifications.Count; i++ )
			{
				int tryMaxCount = 3;
				bool result = false;
				do
				{
					try
					{
						await _repository.Delete(stockNotifications[i]);
					}
					catch( Exception ex )
					{
						_log.LogError(ex, ex.Message);
					}
					tryMaxCount--;
				} while( !result && tryMaxCount > 0 );
				if( !result )
				{
					_log.LogWarning("Notification ( {id} ) could not deleted", stockNotifications[i].Id);
				}
			}
			return new SuccessResult();
		}
		#endregion
	}
}
