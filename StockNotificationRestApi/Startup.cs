using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockNotificationRestApi.Dal.Contexts.EntityFramework;
using StockNotificationRestApi.Dal.Repositories.Abstracts;
using StockNotificationRestApi.Dal.Repositories.Concretes;

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
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			#endregion

			services.AddControllers();

			services.AddScoped<IStockNotificationRepository, EfStockNotificationRepository>();
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
