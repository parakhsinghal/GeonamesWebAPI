using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace GeoDataAPI.Service
{
    public class CacheAttribute:ActionFilterAttribute
    {
        public double MaxAgeInSeconds { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (this.MaxAgeInSeconds > 0)
            {
                actionExecutedContext.Response.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue()                
                {
                    MaxAge = TimeSpan.FromSeconds(this.MaxAgeInSeconds),
                    MustRevalidate = true,
                    Private = true
                };

            }
        }
    }
}