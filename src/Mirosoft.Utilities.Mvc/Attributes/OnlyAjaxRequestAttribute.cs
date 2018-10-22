using System;
using System.Reflection;
using System.Web.Mvc;

namespace Mirosoft.Utilities.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class OnlyAjaxRequestAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext context, MethodInfo info)
        {
            return context.HttpContext.Request.IsAjaxRequest();
        }
    }
}
