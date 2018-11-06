using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.API.Controllers.Items;
using Order.API.Controllers.Orders;
using Order.API.Controllers.Users;
using Order.API.Helpers;
using Order.Services;
using Order.Services.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace Order.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddSingleton<IUserService, UserService>();
			services.AddSingleton<IItemService, ItemService>();
			services.AddSingleton<IOrderService, OrderService>();

			services.AddSingleton<UserMapper>();
			services.AddSingleton<ItemMapper>();
			services.AddSingleton<OrderMapper>();
			services.AddSingleton<OrderItemMapper>();

			services.AddAuthentication("BasicAuthentication")
				.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

			services.AddAuthorization(options =>
			{
				options.AddPolicy("Customer", policy => policy.RequireRole("Customer", "Admin"));
				options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Order API", Version = "v1" });
			});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseHttpsRedirection();

			app.UseAuthentication();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API");
				c.DefaultModelExpandDepth(2);
				c.DefaultModelRendering(ModelRendering.Example);
				c.DefaultModelsExpandDepth(-1);
				c.DisplayOperationId();
				c.DisplayRequestDuration();
				c.DocExpansion(DocExpansion.None);
				c.EnableDeepLinking();
				c.EnableFilter();
				c.MaxDisplayedTags(5);
				c.ShowExtensions();
				c.EnableValidator();
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseMvc();
			
		}
	}
}
