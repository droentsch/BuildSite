using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace WebBuildSite.Extensions
{
    public static class ClientIdHelper
    {
        public static String GetIdFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return TagBuilder.CreateSanitizedId(ExpressionHelper.GetExpressionText(expression));
        }
        public static String GetIdFor(this HtmlHelper htmlHelper, string expression)
        {
            return TagBuilder.CreateSanitizedId(expression);
        }
    }
}