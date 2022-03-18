using Microsoft.AspNetCore.Mvc.Filters;

namespace GlobalLoggingWithSerilog
{
    public class Helpers
    {

        internal static void GetLocationForApiCall(ActionExecutingContext context, Dictionary<string, object> dictionary, out string location)
        {

            var method = context.HttpContext.Request.Method.ToUpper();

            var routeTemplate = context.ActionDescriptor.AttributeRouteInfo?.Template;

           foreach (var key in context.RouteData.Values.Keys)
            {
                var value = context.RouteData.Values[key]?.ToString();

                if(Int64.TryParse(value, out long numeric))
                {
                    dictionary.Add($"Route-{key}", value.ToString());
                }
                else
                {
                    routeTemplate = routeTemplate?.Replace("{" + key + "}", value);
                }

            }


            location = $"{method} {routeTemplate}";
        }
    }
}
