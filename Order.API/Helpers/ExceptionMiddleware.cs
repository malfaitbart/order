using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Order.Domain.ErrorHandling;
using Order.Services.Interfaces;
using System;
using System.Net;

namespace Order.API.Helpers
{
	public static class ExceptionMiddleware
	{
		public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerService logger)
		{
			app.UseExceptionHandler(appError =>
			{
				appError.Run(async context =>
				{
					//context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.ContentType = "application/json";

					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
					if (contextFeature != null)
					{
						Guid guid = Guid.NewGuid();
						string errormessage = $"Error ID: {guid} => {contextFeature.Error.Message}";
						logger.LogError(errormessage);

						await context.Response.WriteAsync(new ErrorDetails()
						{

							StatusCode = context.Response.StatusCode,
							Message = errormessage
						}.ToString());
					}
				});
			});
		}
	}
}
