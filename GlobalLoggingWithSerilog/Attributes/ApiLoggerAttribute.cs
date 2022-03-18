using Microsoft.AspNetCore.Mvc.Filters;

namespace GlobalLoggingWithSerilog.Attributes
{
    public class ApiLoggerAttribute : IActionFilter
    {
        private Tracker _tracker;

        public void OnActionExecuting(ActionExecutingContext context)
        {

            var dictionary = new Dictionary<string, object>();

            string userId = String.Empty, userName = String.Empty;

            var user = context.HttpContext.User.Identity?.Name;

            dictionary.Add("Usuario ->", user!);

            string location;

            Helpers.GetLocationForApiCall(context, dictionary, out location);

            var qs = context.HttpContext.Request.Query.
                ToDictionary(kv => kv.Key, kv => (object) kv.Value, 
                StringComparer.OrdinalIgnoreCase);
          
            var i = 0;

            foreach (var q in qs)
            {
                var newKey = string.Format("q-{0}-{1}", i++, q.Key);

                if (!dictionary.ContainsKey(newKey))
                {
                    dictionary.Add(newKey, q.Value);
                }

            }

            var referer = context.HttpContext.Request.Path.ToString();

            var source = string.Empty;

            if (referer.ToLower().Contains("swagger"))
            {
                source = "Swagger";
            } else
            {
                source = referer;
            }

            if (!dictionary.ContainsKey("Referer"))
            {
                dictionary.Add("Referer", source);
            }


            _tracker = new Tracker(location, user, location, "", "API", dictionary);
           
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

            try
            {
                if (_tracker != null)
                {
                    _tracker.Stop(); 
                }
            } catch (Exception ex)
            {

            }
        }
    }
}
