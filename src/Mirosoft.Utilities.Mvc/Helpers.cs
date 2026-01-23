using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Encodings.Web;

namespace Mirosoft.Utilities.Mvc
{
	public static class Helpers
	{
		public static IHtmlContent CheckBoxList<T>(this IHtmlHelper helper, string name, IEnumerable<T> items, string textField, string valueField,
												   IEnumerable<T> selectedItems = null, object htmlAttributes = null)
		{
			var itemstype = typeof(T);
			var textfieldInfo = itemstype.GetProperty(textField, typeof(string));
			var valuefieldInfo = itemstype.GetProperty(valueField);

			var checklist = new StringBuilder();

			foreach (var item in items)
			{
				var tag = new TagBuilder("input");
				tag.Attributes["type"] = "checkbox";
				tag.Attributes["value"] = valuefieldinfo_safe(valuefieldInfo, item);
				tag.Attributes["name"] = name;

				if (selectedItems != null && selectedItems.Contains(item))
				{
					tag.Attributes["checked"] = "checked";
				}

				if (htmlAttributes != null)
				{
					foreach (var property in htmlAttributes.GetType().GetProperties())
					{
						tag.MergeAttribute(property.Name, property.GetValue(htmlAttributes)?.ToString(), true);
					}
				}

				// input is self-closing; render input and then label/text
				tag.TagRenderMode = TagRenderMode.SelfClosing;
				using (var writer = new StringWriter())
				{
					tag.WriteTo(writer, HtmlEncoder.Default);
					checklist.Append(writer.ToString());
				}

				var text = textfieldInfo?.GetValue(item, null)?.ToString() ?? string.Empty;
				checklist.Append(HtmlEncoder.Default.Encode(text));
				checklist.Append("<br />");
			}

			return new HtmlString(checklist.ToString());

			static string valuefieldinfo_safe(System.Reflection.PropertyInfo info, object target)
			{
				var v = info?.GetValue(target, null);
				return v?.ToString() ?? string.Empty;
			}
		}

		public static IHtmlContent CheckBoxList(this IHtmlHelper helper, string name, IEnumerable<SelectListItem> items,
												 IEnumerable<string> selectedItems = null, string checkboxStyle = "margin-right:5px;", string labelStyle = "")
		{
			var checklist = new StringBuilder();

			foreach (var item in items)
			{
				var containerDiv = new TagBuilder("div");

				var input = new TagBuilder("input");
				input.Attributes["type"] = "checkbox";
				input.Attributes["name"] = name;
				input.Attributes["value"] = item.Value;
				if (!string.IsNullOrEmpty(checkboxStyle))
					input.Attributes["style"] = checkboxStyle;

				if (selectedItems != null && selectedItems.Contains(item.Value))
					input.Attributes["checked"] = "checked";

				input.TagRenderMode = TagRenderMode.SelfClosing;

				var labelTag = new TagBuilder("label");
				if (!string.IsNullOrEmpty(labelStyle))
					labelTag.Attributes.Add("style", labelStyle);
				labelTag.InnerHtml.Append(item.Text);

				// compose input + label inside container
				using (var w = new StringWriter())
				{
					input.WriteTo(w, HtmlEncoder.Default);
					labelTag.WriteTo(w, HtmlEncoder.Default);
					containerDiv.InnerHtml.AppendHtml(w.ToString());
				}

				using (var w2 = new StringWriter())
				{
					containerDiv.WriteTo(w2, HtmlEncoder.Default);
					checklist.Append(w2.ToString());
				}
			}

			return new HtmlString(checklist.ToString());
		}

		public static IHtmlContent IconActionLink(this IHtmlHelper helper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, string iconClass)
		{
			var factory = helper.ViewContext.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
			var urlHelper = factory.GetUrlHelper(helper.ViewContext);

			var link = BuildIconLink(linkText, htmlAttributes, iconClass);
			link.Attributes["href"] = urlHelper.Action(actionName, controllerName, routeValues);

			using var writer = new StringWriter();
			link.WriteTo(writer, HtmlEncoder.Default);
			return new HtmlString(writer.ToString());
		}

		public static IHtmlContent IconActionLink(this IHtmlHelper helper, string linkText, string routeName, object routeValues, object htmlAttributes, string iconClass)
		{
			var urlHelper = new UrlHelper(helper.ViewContext);
			var link = BuildIconLink(linkText, htmlAttributes, iconClass);
			link.Attributes["href"] = urlHelper.RouteUrl(routeName, routeValues);

			using var writer = new StringWriter();
			link.WriteTo(writer, HtmlEncoder.Default);
			return new HtmlString(writer.ToString());
		}

		private static TagBuilder BuildIconLink(string linkText, object htmlAttributes, string iconClass)
		{
			var icon = new TagBuilder("i");
			icon.AddCssClass(iconClass);

			var span = new TagBuilder("span");
			span.InnerHtml.Append(linkText);

			var linkTag = new TagBuilder("a");

			using (var w = new StringWriter())
			{
				icon.WriteTo(w, HtmlEncoder.Default);
				span.WriteTo(w, HtmlEncoder.Default);
				linkTag.InnerHtml.AppendHtml(w.ToString());
			}

			if (htmlAttributes != null)
			{
				foreach (var prop in htmlAttributes.GetType().GetProperties())
				{
					linkTag.MergeAttribute(prop.Name, prop.GetValue(htmlAttributes)?.ToString(), true);
				}
			}

			return linkTag;
		}
	}
}