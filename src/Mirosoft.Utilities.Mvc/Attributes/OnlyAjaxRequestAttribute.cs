using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;

namespace Mirosoft.Utilities.Mvc.Attributes
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class OnlyAjaxRequestAttribute : ActionMethodSelectorAttribute
	{
		public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
		{
			return IsAjaxRequest(routeContext.HttpContext.Request);
		}

		private static bool IsAjaxRequest(HttpRequest request)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			return request.Headers != null &&
				   request.Headers["X-Requested-With"] == "XMLHttpRequest";
		}
	}
}