#region USINGS
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using StockNotificationRestApi.Bll.Handlers.Abstracts;
using StockNotificationRestApi.Bll.Handlers.Concretes;
using StockNotificationRestApi.Bll.Services.Abstracts;
using StockNotificationRestApi.Bll.Services.Concretes;
using StockNotificationRestApi.Dal.Contexts.EntityFramework;
using StockNotificationRestApi.Dal.Repositories.Abstracts;
using StockNotificationRestApi.Dal.Repositories.Concretes;
#endregion

namespace StockNotificationRestApi
{
	public class Startup
	{
		#region DEFINES
		public IConfiguration Configuration { get; }
		#endregion

		#region CONSTRUCTOR
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		#endregion

		#region CONFIGURE SERVICE
		public void ConfigureServices(IServiceCollection services)
		{
			#region ADD DB CONTEXT
			services.AddDbContext<StockNotificationContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);
			#endregion

			services.AddControllers();

			services.AddHttpClient();
			services.RemoveAll<IHttpMessageHandlerBuilderFilter>();

			services.AddSingleton<IStockNotificationRepository, EfStockNotificationRepository>();
			services.AddSingleton<IStockNotificationService, StockNotificationManager>();
			services.AddSingleton<INotificationHandler, NotificationHandler>();
		}
		#endregion

		#region CONFIGURE
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if( env.IsDevelopment() )
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
		#endregion
	}
}
