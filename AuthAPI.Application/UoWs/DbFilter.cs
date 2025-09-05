using AsmResolver.DotNet.Resources;
using AuthAPI.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace AuthAPI.Application.UoWs
{
	public class DbFilter : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var result = await next();
			if (result.Exception != null)
			{
				return;
			}
			var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;
			if (actionDesc == null)
			{
				return;
			}
			var uowAttribute=actionDesc.MethodInfo.GetCustomAttribute<DbAttribute>();
			if (uowAttribute == null)
			{
				return;
			}
			foreach(var dbCtxType in uowAttribute.DbContextTypes)
			{
				var dbCtx= context.HttpContext.RequestServices.GetService(dbCtxType) as MySqlDbContext;
				if (dbCtx == null) return;
				await dbCtx.SaveChangesAsync();
			}
		}
	}
}
