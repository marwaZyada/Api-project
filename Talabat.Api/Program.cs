using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Talabat.Api.Errors;
using Talabat.Api.Extentions;
using Talabat.Api.Helpers;
using Talabat.Api.MiddleWares;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repository;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Identity;

namespace Talabat.Api
{
	public class Program
	{
		public  static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
           builder.Services.AddControllers();
           
            builder.Services.AddDbContext<StoreContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DEfaultConnection"));

            });
			builder.Services.AddDbContext<AppIdentityDbContext>(Options =>
			{
				Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));

			});
			builder.Services.AddApplicationServices();
			builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
			{
				var Connection = builder.Configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect(Connection);

			});
			builder.Services.AddIdentityServices(builder.Configuration);

            
			builder.Services.SwaggerService();
            var app = builder.Build();
			#region update DataBase
		using	var Scope=app.Services.CreateScope();
			var Services=Scope.ServiceProvider;

			
			var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
			try
			{
				var DbContext = Services.GetRequiredService<StoreContext>();
				await DbContext.Database.MigrateAsync();
			await StoreContextSeed.SeedAsync(DbContext);

				var appIdentityDbContext = Services.GetRequiredService<AppIdentityDbContext>();
				await appIdentityDbContext.Database.MigrateAsync();

				var userManager = Services.GetRequiredService<UserManager<ApplicationUser>>();
				await AppIdentityDbContextSeed.SeedUserAsync(userManager);

            }
			catch(Exception ex)
			{
				var Logger=LoggerFactory.CreateLogger<Program>();
				Logger.LogError(ex, "Error occured during updating database");
				
			}
			#endregion
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMiddleware<ExceptionMiddleWare>();
				app.UseSwaggerMiddleWares();
			}
			app.UseStatusCodePagesWithRedirects("/Errors/{0}");
			app.UseStaticFiles();
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}