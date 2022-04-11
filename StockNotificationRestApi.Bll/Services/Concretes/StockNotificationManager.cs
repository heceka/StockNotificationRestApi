using System;
using System.Threading.Tasks;
using StockNotificationRestApi.Bll.Services.Abstracts;
using StockNotificationRestApi.Core.Resources;
using StockNotificationRestApi.Core.Utilities.Results.Abstracts;
using StockNotificationRestApi.Core.Utilities.Results.Concretes;
using StockNotificationRestApi.Dal.Repositories.Abstracts;
using StockNotificationRestApi.Entities.Concretes;
using StockNotificationRestApi.Entities.DTOs;

namespace StockNotificationRestApi.Bll.Services.Concretes
{
	public class StockNotificationManager : IStockNotificationService
	{
		#region DEFINES
		private readonly IStockNotificationRepository _repository;
		#endregion

		#region CONSTRUCTOR
		public StockNotificationManager(IStockNotificationRepository repository)
		{
			_repository = repository;
		}
		#endregion

		#region GET BY PRODUCT ID AND USER ID
		public async Task<IDataResult<StockNotification>> GetByProductIdAndUserId(string productId, string userId)
		{
			StockNotification entity = await _repository.Get(x => x.ProductId == productId && x.UserId == userId);
			if( entity is null )
			{
				return new ErrorDataResult<StockNotification>(StaticMessages.StockNotificationNotFound);
			}
			return new SuccessDataResult<StockNotification>(entity);
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

			StockNotification newEntity = new StockNotification
			{
				ProductId = model.ProductId,
				UserId = model.UserId
			};
			await _repository.Add(newEntity);
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
			//return new ErrorResult(dataResult.MessageResourceName);
		}
		#endregion
	}
}
