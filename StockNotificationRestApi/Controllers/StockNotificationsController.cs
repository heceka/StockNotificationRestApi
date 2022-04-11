using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockNotificationRestApi.Bll.Services.Abstracts;
using StockNotificationRestApi.Core.Utilities.Results.Abstracts;
using StockNotificationRestApi.Entities.DTOs;

namespace StockNotificationRestApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StockNotificationsController : ControllerBase
	{
		#region DEFINES
		public readonly ILogger<StockNotificationsController> _logger;
		public readonly IStockNotificationService _service;
		#endregion

		#region CONSTRUCTOR
		public StockNotificationsController(ILogger<StockNotificationsController> logger, IStockNotificationService service)
		{
			_logger = logger;
			_service = service;
		}
		#endregion

		#region ADD
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] StockNotificationDto model)
		{
			if( model != null )
			{
				try
				{
					IResult result = await _service.Add(model);
					if( result.Success )
					{
						return Ok();
					}
					return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
				}
				catch( Exception ex )
				{
					_logger.LogError(ex, $"ProductId: {model.ProductId} - UserId: {model.UserId}");
					return StatusCode(StatusCodes.Status500InternalServerError);
				}
			}

			return BadRequest();
		}
		#endregion

		#region REMOVE
		[HttpPost]
		public async Task<IActionResult> Remove([FromBody] StockNotificationDto model)
		{
			if( model != null )
			{
				try
				{
					IResult result = await _service.Remove(model);
					if( result.Success )
					{
						return Ok();
					}
					return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
				}
				catch( Exception ex )
				{
					_logger.LogError(ex, $"ProductId: {model.ProductId} - UserId: {model.UserId}");
					return StatusCode(StatusCodes.Status500InternalServerError);
				}
			}

			return BadRequest();
		}
		#endregion

		[HttpPost]
		public async Task<IActionResult> Trigger(StockNotificationTrigger model)
		{
			return Ok();
		}

	}
}
