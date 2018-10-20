using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Mirosoft.Utilities.Mvc
{
    public static class Helpers
    {
        public static MvcHtmlString CheckBoxList<T>(this HtmlHelper helper, String name, IEnumerable<T> items, String textField, String valueField,

                                                   IEnumerable<T> selectedItems = null, object htmlAttributes = null)

        {

            var itemstype = typeof(T);

            var textfieldInfo = itemstype.GetProperty(textField, typeof(String));

            var valuefieldInfo = itemstype.GetProperty(valueField);



            TagBuilder tag;

            var checklist = new StringBuilder();

            foreach (var item in items)

            {

                tag = new TagBuilder("input");

                tag.Attributes["type"] = "checkbox";

                tag.Attributes["value"] = valuefieldInfo.GetValue(item, null).ToString();

                tag.Attributes["name"] = name;

                if (selectedItems != null && selectedItems.Contains(item))

                {

                    tag.Attributes["checked"] = "checked";

                }

                if (htmlAttributes != null)

                {

                    foreach (var property in htmlAttributes.GetType().GetProperties())

                    {

                        tag.Attributes.Add(property.Name, property.GetValue(htmlAttributes).ToString());

                    }

                }

                tag.InnerHtml = textfieldInfo.GetValue(item, null).ToString();

                checklist.Append(tag.ToString());

                checklist.Append("<br />");

            }

            return MvcHtmlString.Create(checklist.ToString());

        }



        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, String name, IEnumerable<SelectListItem> items,

                                                 IEnumerable<string> selectedItems = null, string checkboxStyle = "margin-right:5px;", string labelStyle = "")



        {

            TagBuilder checkTag;

            TagBuilder labelTag;

            TagBuilder containerDiv;



            var checklist = new StringBuilder();

            foreach (var item in items)

            {

                containerDiv = new TagBuilder("div");



                checkTag = new TagBuilder("input");

                checkTag.Attributes["type"] = "checkbox";

                checkTag.Attributes["name"] = name;

                checkTag.Attributes["value"] = item.Value;

                checkTag.Attributes["style"] = checkboxStyle;



                if (selectedItems != null && selectedItems.Contains(item.Value))

                {

                    checkTag.Attributes["checked"] = "checked";

                }

                labelTag = new TagBuilder("label");

                labelTag.Attributes.Add("style", labelStyle);

                labelTag.InnerHtml = item.Text;



                checkTag.InnerHtml = labelTag.ToString();



                containerDiv.InnerHtml = checkTag.ToString();



                checklist.Append(containerDiv.ToString());

            }

            return MvcHtmlString.Create(checklist.ToString());

        }



        public static MvcHtmlString IconActionLink(this HtmlHelper helper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, string iconClass)

        {

            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);



            var link = IconActionLink(linkText, htmlAttributes, iconClass);



            link.MergeAttribute("href", urlHelper.Action(actionName, controllerName, routeValues));



            if (htmlAttributes != null)

            {

                foreach (var prop in htmlAttributes.GetType().GetProperties())

                {

                    link.MergeAttribute(prop.Name, prop.GetValue(htmlAttributes).ToString());

                }

            }



            return MvcHtmlString.Create(link.ToString());

        }



        public static MvcHtmlString IconActionLink(this HtmlHelper helper, string linkText, string routeName, object routeValues, object htmlAttributes, string iconClass)

        {

            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);



            var link = IconActionLink(linkText, htmlAttributes, iconClass);



            link.MergeAttribute("href", urlHelper.RouteUrl(routeName, routeValues));



            return MvcHtmlString.Create(link.ToString());

        }



        private static TagBuilder IconActionLink(string linkText, object htmlAttributes, string iconClass)

        {

            var icon = new TagBuilder("i");

            icon.AddCssClass(iconClass);



            var span = new TagBuilder("span");

            span.SetInnerText(linkText);



            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);



            var linkTag = new TagBuilder("a");

            linkTag.InnerHtml = $"{icon.ToString()} {span.ToString()}";



            if (htmlAttributes != null)

            {

                foreach (var prop in htmlAttributes.GetType().GetProperties())

                {

                    linkTag.MergeAttribute(prop.Name, prop.GetValue(htmlAttributes).ToString());

                }

            }



            return linkTag;

        }
    }
}
